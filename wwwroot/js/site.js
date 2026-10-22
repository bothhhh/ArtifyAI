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


/* =========================================
   2. AI TOOLS CATEGORY FILTER
========================================= */

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


/* =========================================
   3. LIKE BUTTON WITHOUT PAGE RELOAD
========================================= */

document.addEventListener("submit", async function (event) {
    const form = event.target;

    if (!form.matches("form.ai-like-form")) {
        return;
    }

    // Stop the browser from refreshing the page.
    event.preventDefault();

    const button = form.querySelector('button[type="submit"]');

    if (!button || button.disabled) {
        return;
    }

    button.disabled = true;

    try {
        const response = await fetch(form.action, {
            method: "POST",
            body: new FormData(form),
            credentials: "same-origin",
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            }
        });

        if (!response.ok) {
            throw new Error(
                "Like request failed: " + response.status
            );
        }

        const result = await response.json();

        if (!result.success) {
            throw new Error("Like was not saved.");
        }

        // Find the image card containing the Like button.
        const card = form.closest(".gallery-card");

        // Update the Likes count.
        const count = card
            ? card.querySelector(".ai-like-count span")
            : document.querySelector(".ai-like-count span");

        if (count) {
            count.textContent = result.likes;
        }

        // Update the button.
        button.textContent = "❤️ Liked";
        button.disabled = true;

    } catch (error) {
        console.error("Like error:", error);

        button.disabled = false;

        alert("Could not save your Like. Please try again.");
    }
});

console.log("ArtifyAI JavaScript loaded!");