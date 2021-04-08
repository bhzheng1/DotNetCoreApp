$(window).on("load", function () {
    $.ajax({
        type: "Get",
        url: '/Razor/GetPartialViewResult',
        data: null,

        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data, status) {
            //Use append to add it to the div and not overwrite it 
            //if you have other data in your container
            $('#containerId').append(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
});