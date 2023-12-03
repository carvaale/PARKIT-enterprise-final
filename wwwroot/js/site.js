// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Update the min attribute of the EndTime input element to be the same as the StartTime input element, created by Frank Carusi
function updateEndTimeMin() {
    var startTime = document.getElementById("StartTime").value;
    document.getElementById("EndTime").min = startTime;
}

