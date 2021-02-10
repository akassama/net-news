// document ready function
$(document).ready(function () {

    //open file select on profile image click
    $('#ProfilePicDiv').on('click', function (e) {
        $('#ProfileSelect').trigger('click');
    });
    //set profile image on profile image click
    $('#ProfileSelect').change(function (e) {
        var fileName = e.target.files[0].name;
        $("#ImageFileName").text(fileName);

        var reader = new FileReader();
        reader.onload = function (e) {
            // get loaded data and render thumbnail.
            $(".primary-pfofile-image").attr('src', e.target.result);
        };
        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
    });


    //set post edit image preview 
    if ($('#PostImage').val() != "" && $('#ImageDirectory').val() != "") {

        //get image link
        var imageSrc = "/files/images/" + $('#ImageDirectory').val() + "/" + $('#PostImage').val();

        //set image src
        $('#PreviewImage').attr("src", imageSrc);
    }

    //set modal value on click events
    //Approve Post Modal
    $(".confirm-approve-post").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');	
        $("#ModalApprovePostID").val(inputId);
        $('#confirmApprovePostModal').modal('show');
    });

    //Comment on Post Modal
    $(".comment-post").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');	
        $("#ModalCommentPostID").val(inputId);
        $('#commentPostModal').modal('show');
    });

    //Accept Account Modal
    $(".approve-account").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalApproveAccountID").val(inputId);
        $('#confirmAccountApproveModal').modal('show');
    });

    //Reject Account Modal
    $(".reject-account").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalRejectAccountID").val(inputId);
        $('#confirmAccountRejectModal').modal('show');
    });

    //Suspend Account Modal
    $(".suspend-account").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalSuspendAccountID").val(inputId);
        $('#confirmAccountSuspendModal').modal('show');
    });


    //Activate Account Modal
    $(".activate-account").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalActivateAccountID").val(inputId);
        $('#confirmAccountActivateModal').modal('show');
    });

    //Remove Account Modal
    $(".remove-account").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalRemoveAccountID").val(inputId);
        $('#confirmAccountRemoveModal').modal('show');
    });

    //Delete Category Modal
    $(".delete-category").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalDeleteCategoryID").val(inputId);
        $('#confirmCategoryDeleteModal').modal('show');
    });

    //Delete Tag Modal
    $(".delete-tag").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalDeleteTagID").val(inputId);
        $('#confirmTagDeleteModal').modal('show');
    });


    //Edit Site Data Modal
    $(".edit-site-data").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        var siteValue = $(this).data("site-value");

        $("#ModalSiteDataUinqueKey").val(inputId);
        $("#ModalSiteDataValue").val(siteValue);

        $('#editSiteDataModal').modal('show');
    });


    //Delete Post Modal
    $(".delete-post").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalDeletePostID").val(inputId);
        $('#confirmDeletePostModal').modal('show');
    });

    //Delete Advert Modal
    $(".delete-advert").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalDeleteAdvertID").val(inputId);
        $('#confirmAdvertDeleteModal').modal('show');
    });

    //Delete Job Modal
    $(".delete-job").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalDeleteJobID").val(inputId);
        $('#confirmJobDeleteModal').modal('show');
    });

    //Delete Embedded Modal
    $(".delete-embedded-music").click(function () {
        //get clicked element id
        var inputId = this.getAttribute('id');
        $("#ModalDeleteEmbeddedMusicID").val(inputId);
        $('#confirmEmbeddedMusicDeleteModal').modal('show');
    });

    //remote validation for existing email
    $(function () {
        $("#Email").keyup(function () {
            var val = $("#Email").val();
            $.getJSON("/SignUp/CheckUniqueEmail", { key: val }, function (res) {
                if (res == "exists") {
                    $("#CheckEmailExists").html("<strong class='text-danger'>Email already exists, please choose a different email.</strong>");
                    $('#SubmitButton').prop('disabled', true);
                }
                else {
                    $("#CheckEmailExists").html("");
                    $('#SubmitButton').prop('disabled', false);
                }
            });
        });
    });

    //verify passwords match
    $("#ConfirmPassword").keyup(function () {
        var password = $("#Password").val();
        var confirmPassword = $("#ConfirmPassword").val();

        if (password == confirmPassword && password.length > 1) {
            $("#PasswordInfo").html("<strong class='text-success'>Passwords match.</strong>");
            $("#SubmitButton, #UpdatePasswordButton").attr("disabled", false);
        }
        else {
            $("#PasswordInfo").html("<strong class='text-danger'>Passwords do not match!</strong>");
            $("#SubmitButton, #UpdatePasswordButton").attr("disabled", true);
        }
    });


    //remote validation for existing password
    $(function () {
        $("#Password").keyup(function () {
            var val = $("#Password").val();
            $.getJSON("/Admin/CheckExistingPassword", { key: val }, function (res) {
                if (res == "exists") {
                    $("#CheckPasswordExists").html("<strong class='text-danger'>Please choose a different password from the current one.</strong>");
                    $('#UpdatePasswordButton').prop('disabled', true);
                }
                else {
                    $("#CheckPasswordExists").html("");
                    $('#UpdatePasswordButton').prop('disabled', false);
                }
            });
        });
    });

    //remote validation for existing password match
    $(function () {
        $("#CurrentPassword").keyup(function () {
            var val = $("#CurrentPassword").val();
            $.getJSON("/Admin/CheckExistingPassword", { key: val }, function (res) {
                if (res == "exists") {
                    $("#CheckCurrentPassword").html("");
                    $('#UpdatePasswordButton').prop('disabled', true);
                }
                else {
                    $("#CheckCurrentPassword").html("<strong class='text-danger'>Current password does not match with existing password.</strong>");
                    $('#UpdatePasswordButton').prop('disabled', false);
                }
            });
        });
    });

    // Restricts input for each element in the set of matched elements to the given inputFilter.
    (function ($) {
        $.fn.inputFilter = function (inputFilter) {
            return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                if (inputFilter(this.value)) {
                    this.oldValue = this.value;
                    this.oldSelectionStart = this.selectionStart;
                    this.oldSelectionEnd = this.selectionEnd;
                } else if (this.hasOwnProperty("oldValue")) {
                    this.value = this.oldValue;
                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                } else {
                    this.value = "";
                }
            });
        };
    }(jQuery));

    // set input filters.
    $(".integer-only").inputFilter(function (value) {
        return /^-?\d*$/.test(value);
    });

    $(".integer-plus-only").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });

    $(".integer-range").inputFilter(function (value) {
        return /^\d*$/.test(value) && (value === "" || parseInt(value) <= 500);
    });

    $(".float-number").inputFilter(function (value) {
        return /^-?\d*[.,]?\d*$/.test(value);
    });

    $(".currency-number").inputFilter(function (value) {
        return /^-?\d*[.,]?\d{0,2}$/.test(value);
    });

    $(".latin-only").inputFilter(function (value) {
        return /^[a-z]*$/i.test(value);
    });

    $(".letters-only").inputFilter(function (value) {
        return /^[a-zA-Z ]*$/i.test(value);
    });

    $(".hex-text").inputFilter(function (value) {
        return /^[0-9a-f]*$/i.test(value);
    });

    //allows only decimals for input
    $('.decimal-only').keyup(function () {
        var val = $(this).val();
        if (isNaN(val)) {
            val = val.replace(/[^0-9\.]/g, '');
            if (val.split('.').length > 2)
                val = val.replace(/\.+$/, "");
        }
        $(this).val(val);
    });

    $('.alpha-only').bind('keyup blur', function () {
        var node = $(this);
        node.val(node.val().replace(/[^a-z. ]/g, ''));
    }
    );


    // Read a page's GET URL variables and return them as an associative array.
    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    //get complete url
    var url = $(location).attr('href');
    //set active tab if in url parameter
    if (url.indexOf("tab") >= 0) {
        //alert("contains tab");

        //get url parameter for tab
        var selectedTab = getUrlVars()["tab"];

        //if not empty, set tab
        if (selectedTab != '' && selectedTab != 'undefined') {
            $('#' + selectedTab).click();
        }
    }


    //Set SEO Values
    $("#PostTitle").keyup(function () {
        var postTitle = this.value;
        $('#MetaTitle').val(postTitle);
        $('#MetaDescription').val(postTitle);
    });

    $('.form-check-input').click(function () {
        var checkedValue = this.value;
        var metaKeywords = $('#MetaKeywords').val();
        if ($(this).prop("checked") == true) {
            if (typeof metaKeywords !== 'undefined' && metaKeywords != '') {
                metaKeywords += "," + checkedValue;
                metaKeywords = metaKeywords.replace(",,", ",");
                metaKeywords = metaKeywords.replace(",,,", ",");
            }
            else {
                metaKeywords = checkedValue;
            }
            $('#MetaKeywords').val(metaKeywords);
        }
        else {
            var index = metaKeywords.indexOf(checkedValue);
            if (index !== -1) {
                metaKeywords = metaKeywords.replace(checkedValue, "");
                metaKeywords = metaKeywords.replace(",,", ",");
                metaKeywords = metaKeywords.replace(",,,", ",");
            }
            $('#MetaKeywords').val(metaKeywords);
        }

        if (metaKeywords.length > 0) {
            //remove first or last characters if comma
            if (metaKeywords.charAt(0) === ',') {
                metaKeywords = metaKeywords.substring(1);//trim 1st
                $('#MetaKeywords').val(metaKeywords);
            }
            if (metaKeywords.charAt(str.length - 1) === ',') {
                metaKeywords = metaKeywords.substring(0, metaKeywords.length - 1); //trim last
                $('#MetaKeywords').val(metaKeywords);
            }
        }
    });


    //disable buttons on click
    $(".disable-on-click").on('click', function (event) {
        var clickedBtnID = $(this).attr('id');
        //wait 1.5 seconds before disabling button
        var FormID = $(this).parents("form").attr("id");
        if ($("#" + FormID).valid()) {
            setTimeout(
                function () {
                    var button_text = $("#" + clickedBtnID).text();
                    $("#" + clickedBtnID).html("<i class='fas fa-spinner fa-spin'></i> " + button_text);
                    $("#" + clickedBtnID).attr("disabled", true);
                }, 1500);
        }
    });


});


//validate password change inputs
function validatePasswordChangeForm() {
    var CurrentPassword = document.forms["UpdatePasswordForm"]["CurrentPassword"].value;
    var Password = document.forms["UpdatePasswordForm"]["Password"].value;
    var ConfirmPassword = document.forms["UpdatePasswordForm"]["ConfirmPassword"].value;
    if (CurrentPassword == "") {
        $("#CurrentPasswordError").html("Current Password must be filled out");
        return false;
    }
    else {
        $("#CurrentPasswordError").html("");
    }

    if (Password == "") {
        $("#PasswordError").html("Password must be filled out");
        return false;
    }
    else {
        $("#PasswordError").html("");
    }

    if (ConfirmPassword == "") {
        $("#ConfirmPassword").html("Confirm Password must be filled out");
        return false;
    }
    else {
        $("#ConfirmPassword").html("");
    }
}


// Add the following code if you want the name of the file appear on select
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});