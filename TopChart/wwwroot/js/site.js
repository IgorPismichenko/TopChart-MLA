// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let button = document.getElementById('singerButton');
let content = document.getElementById('sortSinger');
let buttonG = document.getElementById('genreButton');
let contentG = document.getElementById('sortGenre');


button.addEventListener('click', function () {
    if (content.style.display === 'none') {
        content.style.display = 'block';
    } else {
        content.style.display = 'none';
    }
});

buttonG.addEventListener('click', function () {
    if (contentG.style.display === 'none') {
        contentG.style.display = 'block';
    } else {
        contentG.style.display = 'none';
    }
});


