// Enhanced Navbar Scroll Effect
window.addEventListener("scroll", function () {
  const navbar = document.querySelector(".navbar");
  if (window.scrollY > 50) {
    navbar.classList.add("scrolled");
  } else {
    navbar.classList.remove("scrolled");
  }
});

// Smooth scrolling for navigation
document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
  anchor.addEventListener("click", function (e) {
    e.preventDefault();
    const target = document.querySelector(this.getAttribute("href"));
    if (target) {
      window.scrollTo({
        top: target.offsetTop - document.querySelector(".navbar").offsetHeight,
        behavior: "smooth",
      });
    }
  });
});

// Scroll down button functionality
document.querySelector(".scroll-down").addEventListener("click", function () {
  window.scrollBy({
    top: window.innerHeight - document.querySelector(".navbar").offsetHeight,
    behavior: "smooth",
  });
});

// Carousel animation
const carousel = document.querySelector("#heroCarousel");
if (carousel) {
  carousel.addEventListener("slide.bs.carousel", function () {
    const activeItem = this.querySelector(
      ".carousel-item.active .hero-content"
    );
    if (activeItem) {
      activeItem.classList.remove("animate__fadeInLeft");
      activeItem.classList.add("animate__fadeOutLeft");
    }

    setTimeout(() => {
      const nextItem = this.querySelector(".carousel-item-next .hero-content");
      if (nextItem) {
        nextItem.classList.add("animate__fadeInRight");
      }
    }, 600);
  });

  carousel.addEventListener("slid.bs.carousel", function () {
    const activeItem = this.querySelector(
      ".carousel-item.active .hero-content"
    );
    if (activeItem) {
      activeItem.classList.remove(
        "animate__fadeOutLeft",
        "animate__fadeInRight"
      );
      activeItem.classList.add("animate__fadeInLeft");
    }
  });
}

// Set active nav item based on scroll position
const sections = document.querySelectorAll("section");
const navItems = document.querySelectorAll(".nav-link");

function updateActiveNav() {
  let current = "";

  sections.forEach((section) => {
    const sectionTop = section.offsetTop;
    const sectionHeight = section.clientHeight;
    const navbarHeight = document.querySelector(".navbar").offsetHeight;

    if (window.pageYOffset >= sectionTop - navbarHeight - 50) {
      current = section.getAttribute("id");
    }
  });

  navItems.forEach((item) => {
    item.classList.remove("active");
    if (item.getAttribute("href") === `#${current}`) {
      item.classList.add("active");
    }
  });
}

window.addEventListener("scroll", updateActiveNav);
window.addEventListener("load", updateActiveNav);

// Initialize tooltips
const tooltipTriggerList = [].slice.call(
  document.querySelectorAll('[data-bs-toggle="tooltip"]')
);
tooltipTriggerList.map(function (tooltipTriggerEl) {
  return new bootstrap.Tooltip(tooltipTriggerEl);
});

// roles and responsibility

// Role Filtering Functionality
document.querySelectorAll(".btn-filter").forEach((button) => {
  button.addEventListener("click", function () {
    // Remove active class from all buttons
    document.querySelectorAll(".btn-filter").forEach((btn) => {
      btn.classList.remove("active");
    });

    // Add active class to clicked button
    this.classList.add("active");

    const filterValue = this.getAttribute("data-filter");
    const roleCards = document.querySelectorAll(".role-grid [data-category]");

    roleCards.forEach((card) => {
      if (
        filterValue === "all" ||
        card.getAttribute("data-category") === filterValue
      ) {
        card.style.display = "block";
        card.classList.add("animate__animated", "animate__fadeIn");
      } else {
        card.style.display = "none";
        card.classList.remove("animate__animated", "animate__fadeIn");
      }
    });
  });
});

// Initialize progress circles
document.querySelectorAll(".progress-circle").forEach((circle) => {
  const percent = circle.getAttribute("data-percent");
  const fill = circle.querySelector(".progress-circle-fill");
  fill.setAttribute("stroke-dasharray", `${percent}, 100`);
  circle.querySelector(".progress-text").textContent = `${percent}%`;
});

// Modal animation
const roleModals = document.querySelectorAll(".modal");
roleModals.forEach((modal) => {
  modal.addEventListener("show.bs.modal", function () {
    const modalContent = this.querySelector(".modal-content");
    modalContent.classList.add("animate__animated", "animate__zoomIn");
  });

  modal.addEventListener("hidden.bs.modal", function () {
    const modalContent = this.querySelector(".modal-content");
    modalContent.classList.remove("animate__animated", "animate__zoomIn");
  });
});



// system feature section

// Feature cards animation
const featureCards = document.querySelectorAll(".feature-card");

featureCards.forEach((card, index) => {
  // Add animation delay
  card.style.animationDelay = `${index * 100}ms`;

  // Add hover effect
  card.addEventListener("mouseenter", () => {
    card.classList.add("animate__animated", "animate__pulse");
  });

  card.addEventListener("mouseleave", () => {
    card.classList.remove("animate__animated", "animate__pulse");
  });
});

// Intersection Observer for scroll animation
const featureObserver = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add("animate__animated", "animate__fadeInUp");
      }
    });
  },
  { threshold: 0.1 }
);

document.querySelectorAll(".feature-card").forEach((card) => {
  featureObserver.observe(card);
});

// Featured card glow effect
const featuredCard = document.querySelector(".feature-card.featured");
if (featuredCard) {
  setInterval(() => {
    featuredCard.style.boxShadow = `0 10px 30px rgba(124, 58, 237, ${
      0.15 + Math.random() * 0.1
    })`;
  }, 3000);
}

