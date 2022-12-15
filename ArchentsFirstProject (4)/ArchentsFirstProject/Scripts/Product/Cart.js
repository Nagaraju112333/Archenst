
function Addtocart(item) {
    var itemId = $(item).attr("itemid")
    var formdata = new FormData();
    formdata.append("ItemId", itemId);
    $.ajax({
        async: true,
        type: "Post",
        contentType: false,
        processData: false,
        data: formdata,
        url: "/Products/ProductPost/",
        success: function (data) {
            if (data.success) {
                $("#Carditgem").text("cart(" + data.Counter + ")");
            }
        },
        error: function () {
            alert("there is something is Wrong....")
        }
    });
}