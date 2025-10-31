document.addEventListener("DOMContentLoaded", function () {
    // Tab switching functionality
    const tabButtons = document.querySelectorAll(".nav-pills .nav-link");
    const roleTitles = document.querySelectorAll("#selectedRole");

    tabButtons.forEach((button) => {
        button.addEventListener("click", function () {
            const role = this.querySelector("b").textContent;

            // Update role title in all forms
            roleTitles.forEach((title) => {
                title.textContent = role;
            });
        });
    });

    // Profile picture upload preview
    document.querySelectorAll('.profile-picture').forEach((input) => {
        input.addEventListener("change", function (e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                const previewId = this.id.replace('ProfilePic', 'uploadPreview');
                const uploadPreview = document.getElementById(previewId) ||
                    this.closest('.upload-label').querySelector('.upload-preview');

                reader.onload = function (event) {
                    uploadPreview.innerHTML = `<img src="${event.target.result}" alt="Preview" style="width: 100%; height: 100%; object-fit: cover; border-radius: 6px;">`;
                };
                reader.readAsDataURL(file);
            }
        });
    });

    // Password strength indicator
    document.querySelectorAll('.password-input').forEach((field) => {
        field.addEventListener("input", function () {
            const password = this.value;
            const strengthBar = this.closest('.form-group').querySelector('.strength-bar');
            const strength = calculatePasswordStrength(password);

            strengthBar.style.width = `${strength.score * 25}%`;

            if (strength.score <= 1) {
                strengthBar.style.backgroundColor = "#ff4444"; // Red for weak
            } else if (strength.score <= 3) {
                strengthBar.style.backgroundColor = "#ffbb33"; // Orange for medium
            } else {
                strengthBar.style.backgroundColor = "#00C851"; // Green for strong
            }
        });
    });

    // Form submission
    document.querySelectorAll('form').forEach((form) => {
        form.addEventListener("submit", function (e) {
            

            // Validate passwords match
            const password = this.querySelector('.password-input').value;
            const confirmPassword = this.querySelector('input[placeholder="Confirm Password"]').value;

            if (password !== confirmPassword) {
                alert("Passwords do not match!");
                return;
            }

            // Simulate form submission
            const submitBtn = this.querySelector('.submit-btn');
            const originalText = submitBtn.textContent;
            submitBtn.textContent = "Registering...";
            submitBtn.disabled = true;
        });
    });

    // Helper functions
    function validateEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }

    function calculatePasswordStrength(password) {
        let score = 0;

        // Length
        if (password.length > 0) score += 1;
        if (password.length >= 8) score += 1;

        // Complexity
        if (/[A-Z]/.test(password)) score += 1;
        if (/[0-9]/.test(password)) score += 1;
        if (/[^A-Za-z0-9]/.test(password)) score += 1;

        return { score: Math.min(score, 4) };
    }
});

// close form functionality
// const closeForm = () => {
//   const formPanel = document.getElementsByClassName("formPanel");
//   formPanel.style.display = "none"; // Hides the form panel
// };

// const openForm = () => {
//   document.getElementById("formPanel").style.display = "block";
// };