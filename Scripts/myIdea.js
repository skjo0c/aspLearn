function myIdea(id) {
    var obj = { userid : id }
    $.ajax({
        method: "post",
        datatype: "Json",
        data: { id: obj.userid },
        url: "Ideas/Index",
        success: function (result) {
            debugger;
            console.log(result.UserIdea[0]);
            console.log(result);
            var t = "";
            var s = "";
                   //$(".ideaViewer").html(result.UserIdea[0].Title);
            for (var i = 0; i < result.UserIdea.length; i++) {
                var id = result.UserIdea[i].IdeasID;
                console.log(id)
                //console.log(result.UserIdea[i]);
                t = result.UserIdea[i].Title;
                d = result.UserIdea[i].Description;
                s += '<div class = "idea_Title">' + t + '</div>' + '<div class = "idea_Description">' + d + '</div>' + '<button class="btn btn-danger del" onclick="delIdea('+id+')"> Delete </button><br />';       
                $(".ideaViewer").html(s);
            }
        }
    });
}

function delIdea(id) {
    var obj = { ideaID : id }
    $.ajax({
        method: "post",
        datatype: "json",
        data: { id: obj.ideaID },
        url: "Ideas/Delete",
        success: function (result) {
            window.location = "/Ideas";
        }
    });
}