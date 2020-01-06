function getSelectedLanguage()
{
  var lang = getSelectedValue("language"); // Возвращает значение выбранного элемента в выпадающем списке "Language"
  return eval("lang_" + lang);
}

function localize()
{
  var lang = getSelectedLanguage();

  // Перебираем все элементы объекта lang_ru_RU
  for(var ctrlId in lang)
  {
    var value = lang[ctrlId];

    // Получить элемент с id
    var ctrl = document.getElementById(ctrlId);

    // Не найден, продолжаем перебор
    if(ctrl == null)
    {
      continue;
    }

    // Найден, определить тип и присвоить значение
    if(ctrl.tagName == "SPAN")
    {
      ctrl.innerText = value;
    }
    else if(ctrl.tagName == "INPUT")
    {
      ctrl.value = value;
    }
  }
}