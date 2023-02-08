// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$.ajax({
    url: 'https://newsapi.org/v2/top-headlines?country=us&apiKey=' + '0c268da4417f43efa3438a353ea21ff9',
    method: 'GET',
    success: function (news) {
        // This function will be called when the AJAX call is successful
        // You can use the "news" variable to access the news data
        // and display it on your HTML page
    },
    error: function (error) {
        // This function will be called when the AJAX call fails
        // You can use the "error" variable to handle the error
    }
});