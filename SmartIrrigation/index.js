main()

async function main() {
    registerEventListeners()
    
    while(true) {
        await updateThresholds();
        
        if (inDemoMode() === false) {
            await updateValuesFromSensors();
            await updateValuesFromForecast();
        }

        await updateMinutesToIrrigate()
        await sleep(1000)
    }
}

function registerEventListeners() {
    //DEMO SWITCH
    const demoSwitch = document.querySelector("#demoMode")
    demoSwitch.addEventListener('input', async() => {
        await postConfigurationForm();
        if (demoSwitch.checked) {
            updateValuesFromSliders()
            await postRangeForm()
            document.querySelectorAll('input[type="range"]').forEach(input => {
                input.disabled = false
            })
        } else {
            await updateValuesFromSensors();
            document.querySelectorAll('input[type="range"]').forEach(input => {
                input.disabled = true
            })
        }
    })

    //SELECTOR
    document.querySelectorAll('select').forEach(selector => {
        selector.addEventListener('input', async() => {
            await postConfigurationForm();
        });
    });

    //SELECTOR
    document.querySelectorAll('#irrigationOutput').forEach(selector => {
        selector.addEventListener('input', async() => {
            await postConfigurationForm();
        });
    });

    //DEMO MODE SLIDER VALUES
    document.querySelectorAll('input[type="range"]').forEach(input => {
        input.addEventListener('input', async() => {
            if (inDemoMode()) {
                updateValuesFromSliders()
                await postRangeForm()
            }
        });
    });
}

async function postConfigurationForm() {
    const form = document.querySelector("#configurationForm")
    const formData = new FormData(form)

    await fetch('http://localhost:5185/Configuration', {
        method: 'POST',
        body: formData
    })
}

async function postRangeForm(){
    const form = document.querySelector("#rangeForm")
    const formData = new FormData(form)
    
    await fetch('http://localhost:5185/Data', {
        method: 'POST',
        body: formData
    })
    await fetch('http://localhost:5185/WeatherForecast', {
        method: 'POST',
        body: formData
    })
}

async function updateMinutesToIrrigate() {
    const response = await fetch("http://localhost:5185/Algorithm")
    const value = await response.json()
    
    const element = document.querySelector("#minutesToIrrigate")
    element.innerText = value
}

async function updateThresholds() {
    const response = await fetch("http://localhost:5185/Threshold")
    const json = await response.json()
    
    const ids = [
        "lowerMoisture",
        "upperMoisture",
        "temperature",
        "windSpeed",
        "humidity"
    ]
    
    ids.forEach(id => {
        const element = document.getElementById(id+"Threshold")
        let value = json[id]
        if (element.classList.contains("percentage")) value = value * 100;
        element.innerText = Math.round(value * 10) / 10;
    })
}

function inDemoMode() {
    const demoSwitch = document.querySelector("#demoMode")
    return demoSwitch.checked
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms))
}

async function updateValuesFromSensors() {
    const response = await fetch("http://localhost:5185/Data")
    const json = await response.json()

    const ids = [
        "temperature",
        "humidity",
        "moisture"
    ]

    ids.forEach(id => {
        const element = document.getElementById(id+'Value')
        let value = json[id]
        if (element.classList.contains("percentage")) value = Math.round(value * 100);
        element.innerText = value;
    })
}

async function updateValuesFromForecast() {
    const response = await fetch("http://localhost:5185/WeatherForecast")
    const json = await response.json()

    const ids = [
        "windSpeed",
        "precipitationProbability",
        "rainfall"
    ]

    ids.forEach(id => {
        const element = document.getElementById(id+'Value')
        let value = json[id]
        if (element.classList.contains("percentage")) value = Math.round(value * 100);
        element.innerText = value;
    })
}

function updateValuesFromSliders() {
    document.querySelectorAll('input[type="range"]').forEach(input => {
        const outputId = input.id + 'Value';
        const output = document.getElementById(outputId);
        let value = input.value
        if (input.classList.contains("percentage")) value = Math.round(value * 100);
        output.innerText = Math.round(value);
    });
}