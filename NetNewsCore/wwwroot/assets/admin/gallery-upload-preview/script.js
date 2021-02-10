$(".imgAdd").click(function () {
    $(this).closest(".row").find('.imgAdd').before('<div class="col-sm-12 col-md-6 col-lg-6 mb-5 imgUp"><div class="imagePreview"></div><div class="form-group"><input type="text" class="form-control letters-only" name="ImgCaption" id="ImgCaption" maxlength="250" placeholder="caption" required></div><label class="btn btn-success btn-block"> <i class="fas fa-image"></i> Select<input type="file" class="uploadFile img" name="PostImages" id="PostImages" value="Upload Photo" accept="image/jpg,image/jpeg" ></label><i class="fa fa-times ImageAction del"></i></div>');
});
$(document).on("click", "i.del", function () {
    $(this).parent().remove();
    //re-enable the plus icon
    $('.imgAdd').css({ pointerEvents: "visible" });
    $('.ImageAction').removeClass('fas fa-slash text-danger');
    $('.ImageAction').addClass('fa fa-plus');
});
$(function () {
    $(document).on("change", ".uploadFile", function () {
        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
                uploadFile.closest(".imgUp").find('.imagePreview').css("background-image", "url(" + this.result + ")");
            }
        }

    });
});


//Limit file upload buttons to n number
$(".ImageAction ").click(function () {
    const totalNum = $('.uploadFile').length;
    //get data values
    const maxImages = $(this).data("max-img");
    if (totalNum >= maxImages) {
        $('.imgAdd').css({ pointerEvents: "none" });

        $('.ImageAction').removeClass('fa fa-plus');
        $('.ImageAction').addClass('fas fa-slash text-danger');
    }
    else {
        $('.ImageAction').removeClass('fas fa-slash text-danger');
        $('.ImageAction').addClass('fa fa-plus');
    }
});