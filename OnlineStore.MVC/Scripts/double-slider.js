const rangeInputs = document.querySelectorAll('.range-inputs input')
const rangeSliders = document.querySelectorAll('.range-sliders input')
const rangeBar = document.querySelector('.double-slider .range')

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

rangeInputs.forEach(input =>
	input.addEventListener('input', e => {
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
	})
)

rangeSliders.forEach(slider =>
	slider.addEventListener('input', e => {
		if (e.target.id === 'minPrice') {
			rangeInputs[0].value = rangeSliders[0].value
			rangeInputs[0].dispatchEvent(new Event('input'))
		} else {
			rangeInputs[1].value = rangeSliders[1].value
			rangeInputs[1].dispatchEvent(new Event('input'))
		}
	})
)
