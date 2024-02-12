namespace InternetShop;

public class Пользователи
{
    public int ID { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Логин { get; set; }
    public string Пароль { get; set; }
    public string Почта { get; set; }
    public string Адрес_доставки { get; set; }
}

public class Товары
{
    public int ID { get; set; }
    public string Наименование { get; set; }
    public int Цена { get; set; }
    public string Название { get; set; }
}

public class Категории
{
    public int ID { get; set; }
    public string Название { get; set; }
}

public class Заказы
{
    public int ID { get; set; }
    public string Логин { get; set; }
    public string Дата_создания { get; set; }
    public string Статус { get; set; }
}

public class Состав_заказа
{
    public int ID_заказа { get; set; }
    public string Наименование { get; set; }
    public int Количество_товара { get; set; }
}

public class Статусы
{
    public int ID { get; set; }
    public string Статус { get; set; }
}