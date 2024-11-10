namespace Zombieland
{
    public class Readme
    {
        /*
            Добавление контроллера на примере CharacterController:
            - 0 Все контроллеры должны наследоваться от абстрактного класса Controller и реализовать свой интерфейс (ICharacterController)
            - 1 в интерфейсе порождающего контроллера IRootController добавить саойство ICharacterController CharacterController { get; }
            - 2 в классе RootController реализовать public ICharacterController CharacterController { get; private set; }
            - 3 в классе RootController в методе
            protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
                {
                    CharacterController = new CharacterController(this, );
                    subsystemsControllers.Add((IController)CharacterController);
                }
                создать экземпляр контроллера и добавить его в список контроллеров подсистем. В конструктор каждого контроллера 
                в качестве аргумента передаём ссылку на порождающий контроллер и List<IController> со ссылками на те контроллеры, которые
                должны быть активированы на момент запуска контроллера, которому принадлежит конструктор, или null, если таких зависимостей нет

                !!! ВАЖНО ref List<IController> subsystemsControllers является референсным и не нуждается в создании

            - 4 Если какой либо из нижеперечисленных методов пуст - оставить в нём соответствующий комментарий

                public EnvironmentController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
                {
                    // This class’s constructor doesn’t have any content yet.
                }

                protected override void CreateHelpersScripts()
                {
                    // This controller doesn’t have any helpers scripts at the moment.
                }

                protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
                {
                    // This controller doesn’t have any subsystems at the moment.
                }
                
            - 5 Для того, чтобы работал GameData Module нужно установить Newton Json
            Ссылка на сайт: https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.2/manual/index.html
            Инструкция для установки: https://www.youtube.com/watch?v=3H6xkl_EsvQ
            После установки нужно перезагрузить Unity    

            - 6  Для создания регдола - можно использовать плагин Ragdoll Helper.
            Для открытия окна настройки Ragdoll Helper - открываем в меню Windows -> BzSoft -> Ragdoll Helper

            - 7 Объемный 3D звук - https://valvesoftware.github.io/steam-audio/#learn-more
            Как пользовать - видеоурок на ютубе - https://www.youtube.com/watch?v=10WhZdLK7zA
            */
    }
}