// Banner Selection JavaScript

document.addEventListener('DOMContentLoaded', function () {
    let bannersLoaded = false;
    let selectedBannerPath = '';

    const showBannersBtn = document.getElementById('showBannersBtn');
    const hideBannersBtn = document.getElementById('hideBannersBtn');
    const bannerGallery = document.getElementById('bannerGallery');
    const customBannerUpload = document.getElementById('customBannerUpload');

    if (!showBannersBtn) return; // Exit if elements don't exist

    // Show/Hide Banner Gallery
    showBannersBtn.addEventListener('click', function () {
        bannerGallery.style.display = 'block';
        this.classList.add('d-none');
        hideBannersBtn.classList.remove('d-none');

        // Load banners only once
        if (!bannersLoaded) {
            loadBanners();
        }
    });

    hideBannersBtn.addEventListener('click', function () {
        bannerGallery.style.display = 'none';
        this.classList.add('d-none');
        showBannersBtn.classList.remove('d-none');
    });

    // Load available banners from server
    function loadBanners() {
        fetch('/Events/GetAvailableBanners')
            .then(response => response.json())
            .then(banners => {
                const container = document.getElementById('bannerContainer');

                if (banners.length === 0) {
                    container.innerHTML = '<p class="text-muted">No default banners available. Please add images to wwwroot/images/banners/</p>';
                    bannersLoaded = true;
                    return;
                }

                container.innerHTML = '';

                banners.forEach(bannerPath => {
                    const img = document.createElement('img');
                    img.src = bannerPath;
                    img.alt = 'Banner option';
                    img.className = 'banner-option';
                    img.dataset.bannerPath = bannerPath;

                    // Check if this is the current banner image
                    const currentImg = document.getElementById('currentBannerImg');
                    if (currentImg && currentImg.src.endsWith(bannerPath)) {
                        img.classList.add('selected');
                        selectedBannerPath = bannerPath;
                    }

                    img.addEventListener('click', function () {
                        // Remove selected class from all banners
                        document.querySelectorAll('.banner-option').forEach(bn => {
                            bn.classList.remove('selected');
                        });

                        // Add selected class to clicked banner
                        this.classList.add('selected');
                        selectedBannerPath = this.dataset.bannerPath;

                        // Update hidden input
                        document.getElementById('selectedBannerInput').value = selectedBannerPath;

                        // Clear file upload if banner is selected
                        if (customBannerUpload) {
                            customBannerUpload.value = '';
                        }

                        // Update preview
                        const currentImg = document.getElementById('currentBannerImg');
                        if (currentImg) {
                            currentImg.src = bannerPath;
                        } else {
                            // Create preview if it doesn't exist
                            const previewDiv = document.createElement('div');
                            previewDiv.className = 'mb-3';
                            previewDiv.innerHTML = '<img src="' + bannerPath + '" alt="Selected Banner" id="currentBannerImg" style="max-width: 100%; max-height: 200px; border-radius: 8px; border: 3px solid #ddd;" /><p class="text-muted mb-0">Selected banner</p>';

                            // Insert after the Banner Image label
                            const label = document.querySelector('.form-group label.control-label');
                            if (label && label.parentElement) {
                                label.parentElement.insertBefore(previewDiv, label.nextSibling);
                            }
                        }
                    });

                    container.appendChild(img);
                });

                bannersLoaded = true;
            })
            .catch(error => {
                console.error('Error loading banners:', error);
                document.getElementById('bannerContainer').innerHTML = '<p class="text-danger">Error loading banners</p>';
            });
    }

    // Clear banner selection if user uploads custom file
    if (customBannerUpload) {
        customBannerUpload.addEventListener('change', function () {
            if (this.files.length > 0) {
                // Clear banner selection
                document.getElementById('selectedBannerInput').value = '';
                document.querySelectorAll('.banner-option').forEach(bn => {
                    bn.classList.remove('selected');
                });
                selectedBannerPath = '';

                // Preview uploaded file
                const file = this.files[0];
                if (file && file.type.startsWith('image/')) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const currentImg = document.getElementById('currentBannerImg');
                        if (currentImg) {
                            currentImg.src = e.target.result;
                        }
                    };
                    reader.readAsDataURL(file);
                }
            }
        });
    }
});