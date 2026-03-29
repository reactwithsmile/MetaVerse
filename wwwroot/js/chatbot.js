(function () {
    const apiEndpoint = '/api/chat/message';

    const btn = document.getElementById('msChatbotButton');
    const panel = document.getElementById('msChatbotPanel');
    const closeBtn = document.getElementById('msChatbotClose');
    const messages = document.getElementById('msChatbotMessages');
    const footer = document.getElementById('msChatbotFooter');
    const label = document.getElementById('msChatbotLabel');

    if (!btn || !panel || !messages || !footer || !label) return;

    let sessionId = null;

    label.style.display = panel.style.display === 'flex' ? 'none' : 'block';

    function openPanel() {
        panel.style.display = 'flex';
        label.style.display = 'none';
        renderInput();
    }
    function closePanel() {
        panel.style.display = 'none';
        label.style.display = 'block';
    }

    function scrollToBottom() {
        messages.scrollTop = messages.scrollHeight;
    }

    function appendMessage(text, from) {
        const el = document.createElement('div');
        el.className = from === 'user' ? 'ms-chatbot-user' : 'ms-chatbot-bot';
        el.textContent = text;
        messages.appendChild(el);
        scrollToBottom();
    }

    function setFooterToTyping() {
        footer.innerHTML = '<div class="ms-chatbot-typing">Assistant is typing…</div>';
    }

    function renderInput() {
        footer.innerHTML = '';
        const form = document.createElement('form');
        form.className = 'd-flex w-100';
        form.addEventListener('submit', (e) => { e.preventDefault(); onSend(e); });

        const input = document.createElement('input');
        input.type = 'text';
        input.placeholder = 'Type a message...';
        input.className = 'form-control me-2';
        input.style.background = 'transparent';
        input.style.border = '1px solid rgba(255,255,255,0.06)';
        input.style.color = '#e6e6f0';

        const send = document.createElement('button');
        send.type = 'submit';
        send.className = 'btn btn-neon';
        send.textContent = 'Send';

        form.appendChild(input);
        form.appendChild(send);
        footer.appendChild(form);

        input.focus();
    }

    async function onSend(e) {
        const input = footer.querySelector('input');
        if (!input) return;
        const text = input.value.trim();
        if (!text) return;

        appendMessage(text, 'user');
        input.value = '';
        setFooterToTyping();

        try {
            const res = await fetch(apiEndpoint, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ Text: text, SessionId: sessionId })
            });
            if (!res.ok) throw new Error('Network error');
            const data = await res.json();
            sessionId = data.sessionId || sessionId;
            appendMessage(data.reply || data.fulfillment || 'No reply', 'bot');
        } catch (err) {
            appendMessage('Error contacting assistant. See console for details.', 'bot');
            console.error(err);
        } finally {
            renderInput();
        }
    }

    // hook events
    btn.addEventListener('click', (e) => {
        e.stopPropagation();
        if (panel.style.display === 'flex') closePanel();
        else openPanel();
    });

    label.addEventListener('click', (e) => { e.stopPropagation(); openPanel(); });

    closeBtn?.addEventListener('click', (e) => { e.stopPropagation(); closePanel(); });

    document.addEventListener('click', function (e) {
        const target = e.target;
        if (!panel.contains(target) && !btn.contains(target) && !label.contains(target) && panel.style.display === 'flex') {
            closePanel();
        }
    });

    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && panel.style.display === 'flex') closePanel();
    });
})();
