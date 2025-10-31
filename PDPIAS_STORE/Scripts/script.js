// =============================================
// Mobile Menu Functionality
// =============================================
const menuToggle = document.getElementById("menuToggle");
const sidebar = document.getElementById("sidebar");

menuToggle.addEventListener("click", function (e) {
    e.stopPropagation();
    sidebar.classList.toggle("show");
});

// Close when clicking outside
document.addEventListener("click", function () {
    if (window.innerWidth < 992) {
        sidebar.classList.remove("show");
    }
});

// Prevent closing when clicking inside sidebar
sidebar.addEventListener("click", function (e) {
    e.stopPropagation();
});

// =============================================
// Navigation System
// =============================================
function showSection(sectionId) {
    // Hide all sections
    document.querySelectorAll(".content-section").forEach(section => {
        section.style.display = "none";
    });

    // Show active section
    const activeSection = document.getElementById(sectionId);
    if (activeSection) {
        activeSection.style.display = "block";

        // Update active nav link
        document.querySelectorAll(".nav-link").forEach(link => {
            link.classList.toggle("active", link.getAttribute("href") === `#${sectionId}`);
        });

        // Update page title
        const activeLink = document.querySelector(`.nav-link[href="#${sectionId}"]`);
        if (activeLink) {
            document.querySelector(".page-title").textContent =
                activeLink.querySelector("span").textContent;
        }
    }
}

// Set up navigation
document.querySelectorAll(".nav-link").forEach(link => {
    link.addEventListener("click", function (e) {
        e.preventDefault();
        const sectionId = this.getAttribute("href").substring(1);
        showSection(sectionId);

        // Close sidebar on mobile
        if (window.innerWidth < 992) {
            sidebar.classList.remove("show");
        }
    });
});

// Show dashboard by default
showSection("dashboard-section");

// =============================================
// Table Enhancements for Mobile
// =============================================
//function enhanceTables() {
//    const tables = document.querySelectorAll(".table-responsive");

//    tables.forEach(table => {
//        // Add accessibility attributes
//        const tableEl = table.querySelector("table");
//        if (tableEl) {
//            tableEl.setAttribute("role", "grid");
//            tableEl.querySelectorAll("th").forEach(th => th.setAttribute("role", "columnheader"));
//            tableEl.querySelectorAll("td").forEach(td => td.setAttribute("role", "gridcell"));
//        }

//        // Touch event handling for mobile scrolling
//        let startX;
//        table.addEventListener("touchstart", function (e) {
//            startX = e.touches[0].clientX;
//        }, { passive: true });

//        table.addEventListener("touchmove", function (e) {
//            if (Math.abs(e.touches[0].clientX - startX) > 5) {
//                e.preventDefault();
//            }
//        }, { passive: false });
//    });
//}

// =============================================
// Chart Initialization
// =============================================
function initCharts() {
    // Usage Chart
    const usageCtx = document.getElementById("usageChart")?.getContext("2d");
    if (usageCtx) {
        new Chart(usageCtx, {
            type: "line",
            data: {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
                datasets: [
                    {
                        label: "Chemicals",
                        data: [65, 59, 80, 81, 56, 72],
                        borderColor: "#4f46e5",
                        backgroundColor: "rgba(79, 70, 229, 0.1)",
                        tension: 0.3,
                        fill: true
                    },
                    {
                        label: "Glassware",
                        data: [28, 48, 40, 19, 36, 27],
                        borderColor: "#10b981",
                        backgroundColor: "rgba(16, 185, 129, 0.1)",
                        tension: 0.3,
                        fill: true
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: "top" },
                    tooltip: { mode: "index", intersect: false }
                },
                scales: {
                    y: { beginAtZero: true, grid: { drawBorder: false } },
                    x: { grid: { display: false } }
                }
            }
        });
    }

    // Inventory Chart
    const inventoryCtx = document.getElementById("inventoryChart")?.getContext("2d");
    if (inventoryCtx) {
        new Chart(inventoryCtx, {
            type: "doughnut",
            data: {
                labels: ["Chemistry", "Physics", "Biology", "Research"],
                datasets: [{
                    data: [65, 48, 29, 37],
                    backgroundColor: ["#4f46e5", "#10b981", "#f59e0b", "#3b82f6"],
                    borderWidth: 0
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: "right" },
                    title: { display: true, text: "Chemical Distribution by Department" }
                }
            }
        });
    }

    // Usage Trend Chart
    const trendCtx = document.getElementById("usageTrendChart")?.getContext("2d");
    if (trendCtx) {
        new Chart(trendCtx, {
            type: "bar",
            data: {
                labels: ["Chemistry", "Physics", "Biology", "Research"],
                datasets: [
                    {
                        label: "Chemicals Used",
                        data: [42, 30, 18, 25],
                        backgroundColor: "#4f46e5"
                    },
                    {
                        label: "Glassware Used",
                        data: [35, 28, 15, 20],
                        backgroundColor: "#10b981"
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: "top" },
                    title: { display: true, text: "Monthly Usage by Department" }
                },
                scales: {
                    y: { beginAtZero: true }
                }
            }
        });
    }
}

// =============================================
// Other Functionality
// =============================================
// Logout functionality
document.querySelectorAll("#logoutBtn, #sidebarLogoutBtn").forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        if (confirm("Are you sure you want to logout?")) {
            alert("You have been logged out. Redirecting to login page...");
            // window.location.href = "/login";
        }
    });
});

// Card animations
const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add("fade-in");
            observer.unobserve(entry.target);
        }
    });
}, { threshold: 0.1 });

document.querySelectorAll(".fade-in").forEach(card => {
    observer.observe(card);
});

// Export button functionality
document.querySelector(".btn-outline-primary")?.addEventListener("click", function (e) {
    e.preventDefault();
    alert("Export functionality would be implemented here");
});

// Profile photo preview
document.getElementById("profileUpload")?.addEventListener("change", function (e) {
    const file = e.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (event) {
            document.getElementById("profilePreview").src = event.target.result;
        };
        reader.readAsDataURL(file);
    }
});

// =============================================
// Initialize Everything
// =============================================
document.addEventListener("DOMContentLoaded", function () {
    initCharts();
    //enhanceTables();

    // Handle window resize
    //let resizeTimer;
    //window.addEventListener("resize", function () {
    //    clearTimeout(resizeTimer);
    //    resizeTimer = setTimeout(enhanceTables, 250);
    //});
});