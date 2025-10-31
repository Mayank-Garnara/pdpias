document.addEventListener('DOMContentLoaded', function () {
    const sendOtpBtn = document.getElementById('sendOtpBtn');
    const countdownEl = document.getElementById('countdown');
    const submitBtn = document.getElementById('submitBtn');
    const otpInputs = document.querySelectorAll('.otp-input');

    // Auto-focus and move between OTP inputs
    otpInputs.forEach((input, index) => {
        input.addEventListener('input', () => {
            if (input.value.length === 1 && index < otpInputs.length - 1) {
                otpInputs[index + 1].focus();
            }
            checkOtpComplete();
        });

        input.addEventListener('keydown', (e) => {
            if (e.key === 'Backspace' && input.value.length === 0 && index > 0) {
                otpInputs[index - 1].focus();
            }
        });
    });

    // Send OTP button functionality
    sendOtpBtn.addEventListener('click', function () {
        // Disable button
        this.disabled = true;

        var email = document.getElementById('email').innerText;
        // Simulate OTP sending (replace with actual API call)
        console.log('OTP sent to ' + email);

        // Start 60 second countdown
        let seconds = 60;
        countdownEl.textContent = `Resend in ${seconds}s`;

        const timer = setInterval(() => {
            seconds--;
            countdownEl.textContent = `Resend in ${seconds}s`;

            if (seconds <= 0) {
                clearInterval(timer);
                countdownEl.textContent = '';
                sendOtpBtn.disabled = false;
            }
        }, 1000);
    });

    // Check if OTP is complete
    function checkOtpComplete() {
        const otpComplete = Array.from(otpInputs).every(input => input.value.length === 1);
        submitBtn.disabled = !otpComplete;
    }

    function getFullOtp() {
        let otp = '';
        for (let i = 1; i <= 6; i++) {
            otp += document.getElementById('otp' + i).value;
        }
        return otp;
    }

    // Form submission
    document.getElementById('registrationForm').addEventListener('submit', function (e) {
        e.preventDefault();

        let fullOtp = getFullOtp();
        document.getElementById('FullOtp').value = fullOtp;

        document.getElementById('registrationForm').submit();
    });
});