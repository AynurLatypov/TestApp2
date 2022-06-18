// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toClipboard(text) {
    console.log(window.location)
    navigator.clipboard.writeText(window.location.origin + '/' + text)
        .then(() => {
        })
        .catch(err => {
            console.log('Something went wrong', err);
        });

}
