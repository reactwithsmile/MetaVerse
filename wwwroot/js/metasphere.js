// Scroll reveal
const revealObserver = new IntersectionObserver((entries) => {
    entries.forEach(e => { if (e.isIntersecting) e.target.classList.add('visible'); });
}, { threshold: 0.1 });

document.querySelectorAll('.reveal, .reveal-left, .reveal-right')
    .forEach(el => revealObserver.observe(el));

// Stagger card animations
document.querySelectorAll('.nft-card').forEach((card, i) => {
    card.style.transitionDelay = `${i * 0.08}s`;
});

// Active nav link
const path = window.location.pathname.toLowerCase();
document.querySelectorAll('.ms-nav-link').forEach(link => {
    const href = link.getAttribute('href')?.toLowerCase() || '';
    if (href !== '/' && path.includes(href.split('/')[1])) {
        link.classList.add('active');
        link.style.color = 'var(--neon-blue)';
    }
});

// Particle canvas (lightweight)
(function () {
    const canvas = document.createElement('canvas');
    canvas.style.cssText = 'position:fixed;inset:0;z-index:0;pointer-events:none;opacity:0.4';
    document.body.prepend(canvas);
    const ctx = canvas.getContext('2d');
    let W, H, particles = [];

    function resize() {
        W = canvas.width = window.innerWidth;
        H = canvas.height = window.innerHeight;
    }

    function init() {
        particles = Array.from({ length: 60 }, () => ({
            x: Math.random() * W, y: Math.random() * H,
            r: Math.random() * 1.5 + 0.5,
            dx: (Math.random() - 0.5) * 0.3,
            dy: (Math.random() - 0.5) * 0.3,
            color: Math.random() > 0.5 ? '#b44fff' : '#00d4ff'
        }));
    }

    function draw() {
        ctx.clearRect(0, 0, W, H);
        particles.forEach(p => {
            ctx.beginPath();
            ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2);
            ctx.fillStyle = p.color;
            ctx.fill();
            p.x += p.dx; p.y += p.dy;
            if (p.x < 0 || p.x > W) p.dx *= -1;
            if (p.y < 0 || p.y > H) p.dy *= -1;
        });
        requestAnimationFrame(draw);
    }

    resize(); init(); draw();
    window.addEventListener('resize', () => { resize(); init(); });
})();

// Page loader: hide after DOM and resources load
(function () {
    function hideLoader() {
        const loader = document.getElementById('msLoader');
        if (!loader) return;
        loader.classList.add('ms-loader-hidden');
        document.body.classList.remove('ms-loading');
    }

    // If the page is already loaded, hide immediately
    if (document.readyState === 'complete') {
        hideLoader();
    } else {
        window.addEventListener('load', () => {
            // small timeout for smooth fade
            setTimeout(hideLoader, 240);
        });
    }
})();
