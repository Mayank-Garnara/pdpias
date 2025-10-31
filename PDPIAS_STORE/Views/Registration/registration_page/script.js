// Role Selection
document.querySelectorAll(".role-card").forEach((card) => {
  card.addEventListener("click", function () {
    document
      .querySelectorAll(".role-card")
      .forEach((c) => c.classList.remove("active"));
    this.classList.add("active");

    const role = this.dataset.role;
    document.getElementById("selectedRole").textContent =
      this.querySelector("h3").textContent;

    // Toggle department field
    document.getElementById("departmentGroup").style.display = [
      "lab_technician",
      "department_head",
    ].includes(role)
      ? "block"
      : "none";

    // Show form panel
    document.getElementById("formPanel").classList.add("active");
  });
});

// Close Form
document.getElementById("closeForm").addEventListener("click", () => {
  document.getElementById("formPanel").classList.remove("active");
  document
    .querySelectorAll(".role-card")
    .forEach((c) => c.classList.remove("active"));
});

// Generate Username
function generateUsername() {
  const name = document.getElementById("name").value.split(" ")[0];
  if (name) {
    const random = Math.floor(Math.random() * 900) + 100;
    document.getElementById(
      "username"
    ).value = `${name.toLowerCase()}${random}`;
  }
}

// Password Strength
document.getElementById("password").addEventListener("input", function (e) {
  const strength = calculateStrength(e.target.value);
  document.getElementById("strengthBar").style.width = strength.width;
  document.getElementById("strengthBar").style.backgroundColor = strength.color;
});

function calculateStrength(password) {
  let strength = { width: "0%", color: "#ef4444" };
  if (password.length >= 8) strength = { width: "60%", color: "#f59e0b" };
  if (password.length >= 12 && /[!@#$%^&*]/.test(password)) {
    strength = { width: "100%", color: "#10b981" };
  }
  return strength;
}

// Form Submission
document
  .getElementById("registrationForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const submitBtn = document.getElementById("submitBtn");
    const originalBtnText = submitBtn.innerHTML;

    // Validation
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirm_password").value;

    if (password !== confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    // Show loader
    submitBtn.innerHTML = '<div class="loader"></div>';
    submitBtn.disabled = true;

    // Simulate API call
    await new Promise((resolve) => setTimeout(resolve, 1500));

    // Reset form
    submitBtn.innerHTML = originalBtnText;
    submitBtn.disabled = false;
    document.getElementById("registrationForm").reset();
    document.getElementById("formPanel").classList.remove("active");
    alert("Registration Successful!");
  });

// Mobile Close
document.addEventListener("click", (e) => {
  if (
    window.innerWidth <= 768 &&
    !e.target.closest(".form-panel") &&
    !e.target.closest(".role-card")
  ) {
    document.getElementById("formPanel").classList.remove("active");
  }
});

// Updated OTP Functionality
const emailField = document.getElementById("email");
const otpButton = document.getElementById("sendOtp");
const otpField = document.getElementById("otp");

// Email validation and OTP button state
emailField.addEventListener("input", () => {
  const isValidEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailField.value);
  otpButton.disabled = !isValidEmail;
});

// OTP Button click handler
otpButton.addEventListener("click", async () => {
  otpButton.innerHTML = "Sending...";
  otpButton.disabled = true;

  // Simulate OTP sending
  await new Promise((resolve) => setTimeout(resolve, 1500));

  otpButton.innerHTML = "Resend OTP";
  otpButton.disabled = false;
  otpField.classList.add("visible");

  // Start countdown timer
  let timeLeft = 30;
  const timerInterval = setInterval(() => {
    otpButton.innerHTML = `Resend (${timeLeft})`;
    timeLeft--;

    if (timeLeft < 0) {
      clearInterval(timerInterval);
      otpButton.innerHTML = "Resend OTP";
      otpButton.disabled = false;
    }
  }, 1000);
});

// OTP Field validation
otpField.addEventListener("input", () => {
  otpField.value = otpField.value.replace(/\D/g, "").slice(0, 6);
});

// Improved OTP Button click handler with loader and prevent spam click
let otpTimer = null;

otpButton.addEventListener("click", async () => {
  if (otpTimer) return; // Prevent multiple clicks

  otpButton.innerHTML = "Sending...";
  otpButton.disabled = true;

  await new Promise((resolve) => setTimeout(resolve, 1500));

  let timeLeft = 30;
  otpButton.innerHTML = `Resend (${timeLeft})`;
  otpButton.disabled = true;

  otpField.classList.add("visible");

  otpTimer = setInterval(() => {
    timeLeft--;
    otpButton.innerHTML = `Resend (${timeLeft})`;

    if (timeLeft <= 0) {
      clearInterval(otpTimer);
      otpTimer = null;
      otpButton.innerHTML = "Resend OTP";
      otpButton.disabled = false;
    }
  }, 1000);
});

// Optional: Show selected profile image preview (future enhancement)
document
  .getElementById("profilePicture")
  .addEventListener("change", function () {
    const preview = document.getElementById("uploadPreview");
    const file = this.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = function (e) {
        preview.innerHTML = `<img src="${e.target.result}" style="width: 80px; height: 80px; border-radius: 50%;" alt="Preview" />`;
      };
      reader.readAsDataURL(file);
    } else {
      preview.innerHTML = `<i class="fas fa-user-circle"></i><span>Click to upload</span>`;
    }
  });
