$( document ).ready(function() {
    
  $(document).on("click", ".browse", function() {
    var file = $(this).parents().find(".file");
    file.trigger("click");
  });


  $('.image-preview-file').change(function(e) {
    var fileName = e.target.files[0].name;
    $(".image-preview").val(fileName);

    var reader = new FileReader();
    reader.onload = function(e) {
    // get loaded data and render thumbnail.
    $("#PreviewImage").attr('src', e.target.result);
    //document.getElementById("PreviewImage").src = e.target.result;
    };
    // read the image file as a data URL.
    reader.readAsDataURL(this.files[0]);
  });

  //set video file name
  $('.media-file').change(function(e) {
    var FileSize = this.files[0].size / 1024 / 1024; // in MB
    if (FileSize > 10) {
      $("#SelectedMediaFileError").text('File size exceeds 10 MB.'); //for clearing with Jquery
      $("#SelectedMediaFile").text("");
      $('#VideoSource').hide(); // hide video div
    } else {
      var thefile = document.getElementById('ArticleMedia');
      var pathname = thefile.value;
      var filename = pathname.replace("C:\\fakepath\\", "");
      $("#SelectedMediaFile").text("File Name: " + filename);
      $("#SelectedMediaFileError").text("");
      
      //set video src  
      let file = e.target.files[0];
      let blobURL = URL.createObjectURL(file);
      $("#VideoSource").attr('src', blobURL);
      $('#VideoSource').show(); //show video preview
    }
  });
    
});


