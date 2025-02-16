document.addEventListener("DOMContentLoaded", function () {
    if (window.location.hash) {
        const element = document.querySelector(window.location.hash);
        if (element) {
            element.scrollIntoView({ behavior: "smooth" });
        }
    }
});
