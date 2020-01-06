RegisterLang();

lang_ru_RU =
{
  lngLanguage: "Язык", // Пара - идентификатор (id) элемента, текст

  lngPreviewSize: "Размер картинки",
  lngPreviewSmall: "Маленький",
  lngPreviewBig: "Большой",
  
  lngSave: "Сохранить",
  lngExit: "Выход"
}

function RegisterLang()
{
  var ctrl = document.getElementById("language");

  ctrl.add(createOption("Russian", "ru_RU"));
}