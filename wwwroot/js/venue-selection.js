// venue-selection.js - Works with server-rendered venues

document.addEventListener('DOMContentLoaded', function () {
    console.log('Venue selection script loaded');

    // Handle virtual event checkbox
    const virtualCheck = document.getElementById('isVirtualCheck');
    const venueSelection = document.getElementById('venueSelection');
    const virtualLocation = document.getElementById('virtualLocation');

    if (virtualCheck) {
        virtualCheck.addEventListener('change', function () {
            if (this.checked) {
                venueSelection.style.display = 'none';
                virtualLocation.style.display = 'block';
                clearVenueSelection();
            } else {
                venueSelection.style.display = 'block';
                virtualLocation.style.display = 'none';
            }
        });
    }

    // Handle venue selection buttons
    const venueButtons = document.querySelectorAll('.select-venue-btn');
    venueButtons.forEach(button => {
        button.addEventListener('click', function () {
            const venueId = this.getAttribute('data-venue-id');
            selectVenue(venueId);
        });
    });

    // Handle change venue button
    const changeVenueBtn = document.getElementById('changeVenueBtn');
    if (changeVenueBtn) {
        changeVenueBtn.addEventListener('click', function () {
            clearVenueSelection();
        });
    }
});

/**
 * Select a venue and load its timeslots
 */
async function selectVenue(venueId) {
    console.log('Selecting venue:', venueId);

    // Find the venue card
    const venueCard = document.querySelector(`.venue-card[data-venue-id="${venueId}"]`);
    if (!venueCard) {
        console.error('Venue card not found');
        return;
    }

    // Get venue details
    const venueName = venueCard.querySelector('h6').textContent;
    const venueInfo = venueCard.querySelector('.venue-card-body p').textContent;

    // Update hidden input
    document.getElementById('selectedVenueId').value = venueId;

    // Show selected venue details
    document.getElementById('selectedVenueName').textContent = venueName;
    document.getElementById('selectedVenueInfo').textContent = venueInfo;

    // Hide venue grid and show selected venue
    document.querySelector('.venue-grid').style.display = 'none';
    document.querySelector('.alert.alert-info').style.display = 'none';
    document.getElementById('selectedVenueDetails').style.display = 'block';

    // Load timeslots
    await loadTimeSlots(venueId);
}

/**
 * Clear venue selection and return to venue grid
 */
function clearVenueSelection() {
    document.getElementById('selectedVenueId').value = '';
    document.getElementById('selectedTimeSlot').value = '';
    document.querySelector('.venue-grid').style.display = 'grid';
    document.querySelector('.alert.alert-info').style.display = 'block';
    document.getElementById('selectedVenueDetails').style.display = 'none';
    document.getElementById('timeSlotContainer').innerHTML = '';
}

/**
 * Load timeslots for a venue
 */
async function loadTimeSlots(venueId) {
    const container = document.getElementById('timeSlotContainer');

    try {
        console.log(`Loading timeslots for venue ${venueId}...`);
        container.innerHTML = '<div class="text-center"><span class="spinner-border spinner-border-sm"></span> Loading timeslots...</div>';

        const response = await fetch(`/api/Venue/${venueId}/timeslots`);

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const timeSlots = await response.json();
        console.log('Timeslots loaded:', timeSlots);

        if (timeSlots.length === 0) {
            container.innerHTML = '<div class="alert alert-warning">No available timeslots for this venue.</div>';
            return;
        }

        displayTimeSlots(timeSlots);

    } catch (error) {
        console.error('Error loading timeslots:', error);
        container.innerHTML = `<div class="alert alert-danger">Error loading timeslots: ${error.message}</div>`;
    }
}

/**
 * Display timeslots grouped by date
 */
function displayTimeSlots(timeSlots) {
    const container = document.getElementById('timeSlotContainer');
    container.innerHTML = '';

    // Group by date
    const grouped = groupTimeSlotsByDate(timeSlots);

    // Create HTML for each date
    Object.keys(grouped).sort().forEach(dateKey => {
        const dateSection = document.createElement('div');
        dateSection.className = 'mb-4';

        const dateHeader = document.createElement('h6');
        dateHeader.className = 'text-primary mb-3';
        dateHeader.textContent = formatDate(dateKey);
        dateSection.appendChild(dateHeader);

        const slotsGrid = document.createElement('div');
        slotsGrid.className = 'time-slots-grid';
        slotsGrid.style.display = 'grid';
        slotsGrid.style.gridTemplateColumns = 'repeat(auto-fill, minmax(200px, 1fr))';
        slotsGrid.style.gap = '10px';

        // Sort slots by start time
        grouped[dateKey].sort((a, b) => a.startTime.localeCompare(b.startTime));

        grouped[dateKey].forEach(slot => {
            const slotBtn = createTimeSlotButton(slot);
            slotsGrid.appendChild(slotBtn);
        });

        dateSection.appendChild(slotsGrid);
        container.appendChild(dateSection);
    });
}

/**
 * Create a timeslot button
 */
function createTimeSlotButton(slot) {
    const btn = document.createElement('button');
    btn.type = 'button';
    btn.className = 'btn btn-outline-primary time-slot-btn';
    btn.setAttribute('data-slot-id', slot.timeSlotId);
    btn.style.padding = '10px';
    btn.style.textAlign = 'center';

    const timeRange = `${formatTime(slot.startTime)} - ${formatTime(slot.endTime)}`;
    btn.innerHTML = `
        <div style="font-weight: 600;">${timeRange}</div>
        <small class="text-muted">${slot.isAvailable ? 'Available' : 'Booked'}</small>
    `;

    if (!slot.isAvailable) {
        btn.disabled = true;
        btn.classList.remove('btn-outline-primary');
        btn.classList.add('btn-outline-secondary');
    }

    btn.addEventListener('click', function () {
        selectTimeSlot(slot.timeSlotId, timeRange);
    });

    return btn;
}

/**
 * Select a timeslot
 */
function selectTimeSlot(slotId, timeRange) {
    console.log('Timeslot selected:', slotId);

    // Update hidden input
    document.getElementById('selectedTimeSlot').value = slotId;

    // Update button states
    document.querySelectorAll('.time-slot-btn').forEach(btn => {
        btn.classList.remove('btn-primary');
        btn.classList.add('btn-outline-primary');
    });

    const selectedBtn = document.querySelector(`[data-slot-id="${slotId}"]`);
    if (selectedBtn) {
        selectedBtn.classList.remove('btn-outline-primary');
        selectedBtn.classList.add('btn-primary');
    }

    // Optional: Show confirmation message
    showTimeSlotConfirmation(timeRange);
}

/**
 * Show timeslot confirmation
 */
function showTimeSlotConfirmation(timeRange) {
    // Remove existing confirmation if any
    const existing = document.getElementById('timeslot-confirmation');
    if (existing) existing.remove();

    const confirmation = document.createElement('div');
    confirmation.id = 'timeslot-confirmation';
    confirmation.className = 'alert alert-success mt-3';
    confirmation.innerHTML = `<strong>✓ Selected:</strong> ${timeRange}`;

    document.getElementById('timeSlotContainer').appendChild(confirmation);
}

/**
 * Group timeslots by date
 */
function groupTimeSlotsByDate(timeSlots) {
    const grouped = {};
    timeSlots.forEach(slot => {
        const date = new Date(slot.slotDate);
        const dateKey = date.toISOString().split('T')[0];
        if (!grouped[dateKey]) {
            grouped[dateKey] = [];
        }
        grouped[dateKey].push(slot);
    });
    return grouped;
}

/**
 * Format date for display
 */
function formatDate(dateKey) {
    const date = new Date(dateKey + 'T00:00:00');
    const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    return date.toLocaleDateString('en-US', options);
}

/**
 * Format time for display
 */
function formatTime(timeSpan) {
    const parts = timeSpan.split(':');
    const hours = parseInt(parts[0]);
    const minutes = parts[1];
    const ampm = hours >= 12 ? 'PM' : 'AM';
    const displayHours = hours % 12 || 12;
    return `${displayHours}:${minutes} ${ampm}`;
}

/**
 * Validate form before submission
 */
function validateVenueSelection() {
    const isVirtual = document.getElementById('isVirtualCheck').checked;

    if (!isVirtual) {
        const venueId = document.getElementById('selectedVenueId').value;
        const timeSlotId = document.getElementById('selectedTimeSlot').value;

        if (!venueId) {
            alert('Please select a venue');
            return false;
        }

        if (!timeSlotId) {
            alert('Please select a time slot');
            return false;
        }
    }

    return true;
}

// Add form validation on submit
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('eventForm');
    if (form) {
        form.addEventListener('submit', function (e) {
            if (!validateVenueSelection()) {
                e.preventDefault();
            }
        });
    }
});