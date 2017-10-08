
//@*<script type="text/javascript">

//    $("#glrPag li a").click(function(event) {
//        event.preventDefault();
//        var pagei = 2;
//        $.getJSON("JsonTest",
//            {page: pagei },
//            function(data) {
//                $('#test').html(data.FilePath);
//                //  $("#img1").attr("src", data.Content);
//                $("#img1").attr("src", "@Url.Action("JsonTest",new{page = 2})");
//                alert("+");
//            });


//    });

//</script> *@

var module = (function () {

   var loading=function loadImg(p) {
        $.getJSON("Home/GetGalleryPage",
            { page: parseInt(p).toString() },
            function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#img" + (i + 1)).attr("src", "Home/GetImage?id=" + data[i]);
                    $("#aimg" + (i + 1)).attr("href", "Home/GetImage?id=" + data[i]);
                }
            });
    }




        //$(document).ready(loading(1));
       $(window).on('load', loading(1));


    
        $("#glrPag li a").click(function (event) {
           
            event.preventDefault();

            var p = this.textContent;
            loading(p);
            
        });
    

    return {
            };
}());
   


//@*<script type="text/javascript">

//    $("#glrPag li a").click(function (event) {
//        var ie = 1;
//        event.preventDefault();
//        @*$("#img"+ie).attr("src", "@Url.Action("JsonTest",new{page = 2})");
//        $(".item").each(function() {
//            $(this).attr("src", "JsonTest?page=" + ie);
//        });
//        ie++;
//    });
//</script> *@