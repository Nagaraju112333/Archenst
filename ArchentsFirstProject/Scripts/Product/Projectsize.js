const SizeBtn = document.querySelectorAll('.size-radio-btn');
let checkBtn = 0;
SizeBtn.forEach((item, i) => {
    item.addEventListener('click', () => {
        SizeBtn[checkBtn].classList.remove('check');
        item.classList.add('check');
        checkBtn = i;
    })
})
document.getElementById("size").innerHTML = value();