﻿#Использовать "model"

Процедура ПриНачалеРаботыСистемы()
	
	ИспользоватьСтатическиеФайлы();
	ИспользоватьАвторизацию();
	ИспользоватьСессии();
	ИспользоватьФоновыеЗадания();
	ИспользоватьКонсольЗаданий("/jobsserver");
	ИспользоватьМаршруты();

	НастроитьФоновыеЗадания();

КонецПроцедуры


Процедура НастроитьФоновыеЗадания()

	ИД1 = ФоновыеЗадания.ВыполнитьЗаданиеОднократно("ОбработчикиФоновых", "АсинхронноеЗадание");

	ЗадержкаВыполнения = Новый ПараметрыОжиданияФоновыхЗаданий();
	ЗадержкаВыполнения.ОжиданиеВСекундах(30);

	ИД2 = ФоновыеЗадания.ВыполнитьОтложенноеЗадание("ОбработчикиФоновых", "АсинхронноеЗаданиеОтложенное", ЗадержкаВыполнения);
	
	Раписание = Новый РасписаниеФоновыхЗаданий();
	Раписание.КаждуюМинуту();
	Сообщить(Раписание.РасписаниеСтрокой);
	ФоновыеЗадания.СоздатьПериодическоеЗаданиеПоРасписанию("ОбработчикиФоновых", "ЗаданиеПоРасписанию", Раписание);

	ФоновыеЗадания.ВыполнитьПодчиненноеЗадание(ИД1,"ОбработчикиФоновых", "АсинхронноеЗаданиеПодчиненноеОднократному");
	ФоновыеЗадания.ВыполнитьПодчиненноеЗадание(ИД2,"ОбработчикиФоновых", "АсинхронноеЗаданиеПодчиненноеОтложенному");

	ИД3 = ФоновыеЗадания.ВыполнитьЗаданиеОднократно("ОбработчикиФоновых", "АсинхронноеЗаданиеВыбрасывающееИсключение");

КонецПроцедуры