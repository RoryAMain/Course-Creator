
$(document).ready(function(){
    $('#middleTest').click(function(){
        $(".rightPane").css("display", "none");
        $(".expandableMiddlePane").css("display", "block");
    });
});

$(document).ready(function(){
    $('#rightTest').click(function(){
        $(".expandableRightPane").css("display", "block");
    });
});

$(document).ready(function(){
    $('#resetTest').click(function(){
        $(".expandableMiddlePane").css("display", "none");
        $(".expandableRightPane").css("display", "none");
        $(".rightPane").css("display", "block");
    });
});