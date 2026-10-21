// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
    const sections = document.querySelectorAll(".reveal-section");

    if (!("IntersectionObserver" in window)) {
        sections.forEach(section => section.classList.add("visible"));
        return;
    }

    const observer = new IntersectionObserver(
        function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("visible");
                } else {
                    entry.target.classList.remove("visible");
                }
            });
        },
        {
            threshold: 0.25
        }
    );

    sections.forEach(function (section) {
        observer.observe(section);
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const filterButtons = document.querySelectorAll(".tool-filter-btn");
    const toolCards = document.querySelectorAll(".tool-card");

    filterButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            const selectedFilter = button.dataset.filter;

            filterButtons.forEach(function (btn) {
                btn.classList.remove("active");
                btn.setAttribute("aria-pressed", "false");
            });

            button.classList.add("active");
            button.setAttribute("aria-pressed", "true");

            toolCards.forEach(function (card) {
                card.hidden = selectedFilter !== "all" &&
                    card.dataset.category !== selectedFilter;
            });
        });
    });
});
