// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// In your Javascript (external .js resource or <script> tag)
$(document).ready(function () {
    $('#app-dropdown').select2();
    $('#httpverb').select2();

    // Get the dropdown element
    const appDropdown = $('#app-dropdown');

    // Add an event listener for when the dropdown value changes
    appDropdown.change(async () => {
        // Get the selected app name
        const appName = appDropdown.val();

        console.log(appName);
        // Make a GET request to the GetMetaData API endpoint
        const response = await fetch(`/api/Auth?AppName=${appName}`);

        // Check if the response is successful
        if (response.ok) {
            // Get the metadata JSON string from the response
            const metadataJson = await response.text();

            // Parse the JSON string into a JavaScript object
            const metadata = JSON.parse(metadataJson);

            // Do something with the metadata object
            console.log(metadata);

            generateForm(metadata);
        } else {
            // Handle the error
            console.error(`Failed to get metadata for ${appName}: ${response.status} ${response.statusText}`);
        }
    });

    $("#my-form").submit(function (event) {
        event.preventDefault();

        // Extract form data into an object
        const formData = $(this).serializeArray().reduce((obj, field) => ({
            ...obj,
            [field.name]: field.value
        }), {});

        // Add the HTTP verb to the form data
        formData["app"] = $('#app-dropdown').val();
        // Add the HTTP verb to the form data
        formData["method"] = $("#httpverb").val();

        // Create an empty headers array
        const headers = [];

        // Loop through each header pair element and add the key-value pairs to the headers object
        $("#header-fields #header-pair").each(function () {
            const key = $(this).find("#header-key input").val();
            const value = $(this).find("#header-value input").val();
            headers.push({ key: key, value: value });
        });

        // Add the headers object to the formData object
        formData["headers"] = headers;

        // Log the form data and headers to the console
        console.log("Form data:", formData);

        $.ajax({
            url: "/api/Auth/Test",
            type: "POST",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log("Success:", data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("Error:", textStatus, errorThrown);
            }
        });
    });

    function clearForm() {
        $("#my-form").empty();
    }

    function generateForm(schema) {
        clearForm();

        var form = $("#my-form");
        $.each(schema.properties, function (key, value) {
            var label = $("<label>").attr("for", key).text(key);
            var input = $("<input>").attr("type", "text").attr("name", key).attr("id", key).addClass("form-control");;
            var container = $("<div>").addClass("row form-group").append(label).append(input);
            form.append(container);
        });

        //Header
        defaultHeaderSection();

        // Add a submit button
        var submitButton = $("<button>").attr("type", "submit").text("Save").addClass("btn btn-primary");
        form.append(submitButton);
        $('#headers').parent().hide();
    }

    function defaultHeaderSection() {
        var form = $("#my-form");
        var headerDiv = $("<div>").attr("id", "header-section");
        var headerTitle = $("<h4>").text("Headers");
        var headerFields = $("<div>").attr("id", "header-fields");
        headerDiv.append(headerTitle, headerFields, addHeaderButton);
        form.append(headerDiv);

        var addHeaderButton = $("<button>").text("Add Header").on("click", function (event) {
            event.preventDefault(); // Prevent form submission
            addHeaderField();
        }).addClass("btn btn-primary");
        form.append(addHeaderButton);
    }

    function addHeaderField() {
        var headerPair = $("<div>").attr("id", "header-pair").addClass("row header-pair");

        var key = "header-key";
        var headerKey = $("<div>").attr("id", key).addClass("col-sm-5");
        var label = $("<label>").attr("for", key).text("Key");
        var input = $("<input>").attr("type", "text").attr("name", key).attr("id", key).addClass("form-control");
        headerKey.append(label, input);

        key = "header-value";
        var headerValue = $("<div>").attr("id", key).addClass("col-sm-5");
        label = $("<label>").attr("for", key).text("value");
        input = $("<input>").attr("type", "text").attr("name", key).attr("id", key).addClass("form-control");
        headerValue.append(label, input);

        key = "delete-btn";
        var deletebutton = $("<div>").attr("id", key).addClass("col-sm-2");
        var addHeaderButton = $("<button>").text("Delete").on("click", deleteHeaderField).addClass("btn btn-danger form-control");
        deletebutton.append(addHeaderButton);
        headerPair.append(headerKey, headerValue, deletebutton);

        $('#header-fields').append(headerPair, "<br/>");
    }

    function deleteHeaderField() {
        $('.header-pair').last().remove();
    }
});