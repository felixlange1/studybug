const flashcard = document.querySelectorAll('.flashcard');

flashcard.forEach((card) => {
    

    
    card.addEventListener('click', () => {
        card.classList.toggle('flipped');
        
        // question.style.display = 'none';
        // answer.style.display = 'block';
    })
})



