$(document).ready(function() {
    $(".faq a").click(function (event) {
        event.preventDefault();
        var answer = $(this).next(".answer");
        if ($(answer).is(":hidden")) {
            $(".faq .answer").not(":hidden").slideToggle(300);
        }
        $(answer).slideToggle(300);
    });
});