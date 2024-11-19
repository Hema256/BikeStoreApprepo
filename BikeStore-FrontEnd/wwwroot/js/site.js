// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    // Code to fetch and display content dynamically goes here
    const content = document.getElementById('content');

    // Example fetching and displaying categories
    fetch('https://localhost:7217/api/Category')
        .then(response => response.json())
        .then(categories => {
            let categoryList = '<ul>';
            categories.forEach(category => {
                categoryList += `<li>${category.name}</li>`;
            });
            categoryList += '</ul>';
            content.innerHTML = categoryList;
        })
        .catch(error => {
            console.error('Error fetching categories:', error);
        });
});
