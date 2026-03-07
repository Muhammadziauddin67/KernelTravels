$(document).ready(function () {
    // Toggle menu functionality
    $('#menu-toggle').on('click', function (e) {
        e.preventDefault();
        $('#sidebar-wrapper').toggleClass('toggled');
        $('#sidebar-overlay').toggleClass('active');
    });

    // Close sidebar when clicking overlay (mobile only)
    $('#sidebar-overlay').on('click', function () {
        $('#sidebar-wrapper').removeClass('toggled');
        $('#sidebar-overlay').removeClass('active');
    });

    // Close sidebar when clicking a nav link on mobile
    $('.sidebar-nav .nav-link').on('click', function (e) {
        if (window.innerWidth <= 768) {
            $('#sidebar-wrapper').removeClass('toggled');
            $('#sidebar-overlay').removeClass('active');
        }

        // Handle section switching
        const section = $(this).data('section');
        if (section && section !== 'logout') {
            e.preventDefault();
            $('.section-content').removeClass('active');
            $('#' + section).addClass('active');

            // Update active nav link
            $('.sidebar-nav .nav-link').removeClass('active');
            $(this).addClass('active');
        }
    });

    // Handle window resize
    $(window).on('resize', function () {
        if (window.innerWidth > 768) {
            $('#sidebar-wrapper').removeClass('toggled');
            $('#sidebar-overlay').removeClass('active');
        }
    });
});


// Initialize Charts
document.addEventListener('DOMContentLoaded', function() {
   
    // Chart 1: Bar Chart - Number of Listings
    const ctxListings = document.getElementById('musicChart').getContext('2d');
    new Chart(ctxListings, {
        type: 'bar',
        data: {
            labels: ['Hotels', 'Restaurants', 'Resorts', 'Destinations', 'Tourism Spots'],
            datasets: [{
                label: 'Number of Listings',
                data: [12, 20, 10, 15, 18], // Replace with SQL data
                backgroundColor: '#17a2b8'
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'top' },
                title: {
                    display: true,
                    text: 'Available Listings Overview',
                    color: '#ffffff',
                    font: { size: 18 }
                }
            }
        }
    });

    // Chart 2: Line Chart - Monthly Visitor Count
    const ctxVisitors = document.getElementById('userGrowthChart').getContext('2d');
    new Chart(ctxVisitors, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
            datasets: [{
                label: 'Monthly Visitors',
                data: [300, 450, 600, 550, 700, 800], // Replace with SQL counts
                borderColor: '#ffc107',
                backgroundColor: 'rgba(255, 193, 7, 0.2)',
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'top' },
                title: {
                    display: true,
                    text: 'Visitor Growth Over Time',
                    color: '#ffffff',
                    font: { size: 18 }
                }
            }
        }
    });

    // Chart 3: Doughnut Chart - Visitors by Country
    const ctxCountry = document.getElementById('languageChart').getContext('2d');
    new Chart(ctxCountry, {
        type: 'doughnut',
        data: {
            labels: ['USA', 'UK', 'India', 'Pakistan', 'Japan', 'Germany'],
            datasets: [{
                label: 'Visitors by Country',
                data: [180, 120, 140, 90, 60, 100], // Replace with SQL counts
                backgroundColor: ['#007bff', '#28a745', '#ffc107', '#dc3545', '#6610f2', '#20c997']
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'top' },
                title: {
                    display: true,
                    text: 'Geographic Visitor Distribution',
                    color: '#ffffff',
                    font: { size: 18 }
                }
            }
        }
    });

   // Chart 4: Pie Chart - Booking Types
const ctxBooking = document.getElementById('bookingChart').getContext('2d');
new Chart(ctxBooking, {
    type: 'pie',
    data: {
        labels: ['Hotels', 'Resorts', 'Restaurants', 'Tours'],
        datasets: [{
            label: 'Bookings',
            data: [300, 150, 100, 200], // Replace with SQL data
            backgroundColor: ['#007bff', '#28a745', '#ffc107', '#dc3545']
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: { position: 'top' },
            title: {
                display: true,
                text: 'Booking Type Distribution',
                color: '#ffffff',
                font: { size: 18 }
            }
        }
    }
});
// Chart 5: Radar Chart - Seasonal Traffic
const ctxSeasonal = document.getElementById('seasonalChart').getContext('2d');
new Chart(ctxSeasonal, {
    type: 'radar',
    data: {
        labels: ['Spring', 'Summer', 'Autumn', 'Winter'],
        datasets: [{
            label: 'Visitors',
            data: [120, 300, 180, 90],
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: '#4bc0c0',
            pointBackgroundColor: '#4bc0c0'
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: { position: 'top' },
            title: {
                display: true,
                text: 'Seasonal Visitor Trends',
                color: '#ffffff',
                font: { size: 18 }
            }
        }
    }
});
// Chart 6: Horizontal Bar - Average Stay Duration by City
const ctxStay = document.getElementById('stayChart').getContext('2d');
new Chart(ctxStay, {
    type: 'bar',
    data: {
        labels: ['New York', 'Paris', 'Dubai', 'Tokyo', 'Lahore'],
        datasets: [{
            label: 'Average Stay (Days)',
            data: [3.5, 4.2, 5, 2.8, 3.1], // Replace with SQL AVG values
            backgroundColor: '#6f42c1'
        }]
    },
    options: {
        indexAxis: 'y',
        responsive: true,
        plugins: {
            legend: { position: 'top' },
            title: {
                display: true,
                text: 'Average Stay Duration per City',
                color: '#ffffff',
                font: { size: 18 }
            }
        }
    }
});

});
