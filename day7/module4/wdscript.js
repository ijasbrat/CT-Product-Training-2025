
const apiKey ="5c37e08e0afc44e09f890159252909";

document.getElementById("searchBtn").addEventListener("click", () => {
  const city = document.getElementById("cityInput").value.trim();
  if (city) {
    fetchCurrentWeather(city);
  }
});

document.getElementById("cityInput").addEventListener("keypress", (e) => {
  if (e.key === "Enter") {
    e.preventDefault();
    document.getElementById("searchBtn").click();
  }
});

async function fetchCurrentWeather(city) {
  const cityNameEl = document.getElementById("cityName");
  const tempEl = document.getElementById("temperature");
  const humidityEl = document.getElementById("humidity");
  const condTextEl = document.getElementById("conditionText");
  const iconEl = document.getElementById("weatherIcon");
  const errorMsg = document.getElementById("errorMsg");


  errorMsg.textContent = "";

  try {
    const url = `https://api.weatherapi.com/v1/current.json?key=${apiKey}&q=${encodeURIComponent(city)}`;
    const response = await fetch(url);

    if (!response.ok) {
      throw new Error("City not found or API error");
    }

    const data = await response.json();


    cityNameEl.textContent = data.location.name + ", " + data.location.country;
    tempEl.textContent = `${data.current.temp_c} °C`;
    humidityEl.textContent = `${data.current.humidity}%`;
    condTextEl.textContent = data.current.condition.text;


    let iconUrl = data.current.condition.icon;
    if (iconUrl.startsWith("//")) {
      iconUrl = "https:" + iconUrl;
    }
    iconEl.src = iconUrl;
    iconEl.alt = data.current.condition.text;
  } catch (error) {

    errorMsg.textContent = error.message;
    cityNameEl.textContent = "-";
    tempEl.textContent = "-";
    humidityEl.textContent = "-";
    condTextEl.textContent = "-";
    iconEl.src = "";
    iconEl.alt = "";
  }
}
