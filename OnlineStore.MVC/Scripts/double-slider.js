const sliderContainer = document.querySelector('.d-slider-container');
let rangeInputs = document.querySelectorAll('.range-inputs input');
let rangeSliders = document.querySelectorAll('.range-sliders input');
let rangeBar = document.querySelector('.double-slider .range');

const updateSelectors = () => {
	rangeInputs = document.querySelectorAll('.range-inputs input');
	rangeSliders = document.querySelectorAll('.range-sliders input');
	rangeBar = document.querySelector('.double-slider .range');
}

const validateInputs = e => {
	let minValue = parseInt(rangeInputs[0].value)
	let maxValue = parseInt(rangeInputs[1].value)

	if (maxValue - minValue < 0 ||
		e.target.value > parseInt(rangeSliders[0].max) ||
		e.target.value < parseInt(rangeSliders[0].min)
	)
		rangeInputs.forEach(input => input.style.borderColor = '#cc0000');
	else
		rangeInputs.forEach(input => input.style.borderColor = '#aaa');
}

const rangeInputsCallback = (e) => {
	validateInputs(e)

	if (e.target.id === 'minPriceInput') {
		rangeSliders[0].value = rangeInputs[0].value
		rangeBar.style.left =
			(rangeInputs[0].value / rangeSliders[0].max) * 100 + '%'
	} else {
		rangeSliders[1].value = rangeInputs[1].value
		rangeBar.style.right =
			100 - (rangeInputs[1].value / rangeSliders[1].max) * 100 + '%'
	}
};

const rangeSlidersCallback = (e) => {
	if (e.target.id === 'minPrice') {
		rangeInputs[0].value = rangeSliders[0].value
		rangeInputs[0].dispatchEvent(new Event('input'))
	} else {
		rangeInputs[1].value = rangeSliders[1].value
		rangeInputs[1].dispatchEvent(new Event('input'))
	}
};

sliderContainer.addEventListener('input', function (e) {
	updateSelectors();

	if (e.target.matches('.range-inputs input')) {
		rangeInputsCallback(e);
	}
	else if (e.target.matches('.range-sliders input')) {
		rangeSlidersCallback(e);
	}
});
