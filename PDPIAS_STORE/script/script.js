// Theme Toggle Functionality
const themeToggle = document.getElementById("theme-toggle");
themeToggle.addEventListener("click", () => {
    document.body.classList.toggle("dark-theme");
    themeToggle.querySelector("i").classList.toggle("fa-moon");
    themeToggle.querySelector("i").classList.toggle("fa-sun");
    localStorage.setItem(
        "theme",
        document.body.classList.contains("dark-theme") ? "dark" : "light"
    );
});

// Load Saved Theme
if (localStorage.getItem("theme") === "dark") {
    document.body.classList.add("dark-theme");
    themeToggle.querySelector("i").classList.replace("fa-moon", "fa-sun");
}

// Lottie Animation Configuration
const animation = lottie.loadAnimation({
    container: document.getElementById("lottie-animation"),
    renderer: "svg",
    loop: true,
    autoplay: true,
    path: "/assets/microscope.json",
});

animation.addEventListener("data_failed", () => {
    console.error("Lottie file load error!");
    document.getElementById("lottie-animation").innerHTML =
        '<p class="text-white">Animation not loaded. Check console</p>';
});
