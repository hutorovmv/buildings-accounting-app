$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
        loop: false,
        margin: 10,
        nav: true,
        navText: [
            '<div class="cntrl bg-light rounded mx-1 p-3"><i class="fas fa-chevron-left text-dark"></i></div>',
            '<div class="cntrl bg-light rounded mx-1 p-3"><i class="fas fa-chevron-right text-dark"></i></div>'
        ],
        responsive: {
            0: {
                items: 1
            }
        }
    });

    $("#add-file-btn").click(function () {
        var input = '<div><input name="files" type="file" class="form-control-file my-2 w-100" /><i class="minus btn btn-sm btn-outline-info fas fa-minus"></i></div>';
        $(input).appendTo("#add-files");
    });

    $(document).on("click", ".minus", function () {
        $(this).parent('div').remove();
    });
});