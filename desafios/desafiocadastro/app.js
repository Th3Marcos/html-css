'use strict'
const switcher = document.querySelector('.btn');

switcher.addEventListener('click', function () {
    document.body.classList.toggle('dark-mode')
    document.body.classList.toggle('light-mode')

    var className = document.body.className;
    if (className == "dark-mode") {
        this.textContent = "Light";
    } else {
        this.textContent = "Dark";
    }

    console.log('current class name: ' + className);
});