// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#addCommentForm").on("submit", function (event) {
        event.preventDefault();
        var formData = $(this).serialize();
        $.ajax({
            type: "POST",
            url: $(this).attr("action"),
            data: formData,
            success: function (response) {
                $("#commentsList").append(response);
                $("#commentContent").val('');
            },
            error: function () {
                alert("Error adding comment.");
            }
        });
    });
});
