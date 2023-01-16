# Lightweight-Dependency-Injection

-------------------------------------------------------------------------RUSSIAN---------------------------------------------------------------------------
Моя легковесная реализация контейнера зависимостей aka DI Container. 

Данный контейнер можно использовать для превращения вашего приложения в набор слабосвязанных частей с сильно сегментированными обязанностями. 
Можно произвести склейку объекта посредством установщиков(конфигураторов) aka installer.
Сами установщики собираются в контейнер aka InstallContainer, позволяющий собрать наборы зависимостей различных объектов.


# Пример 1. Использование отдельного установищика

        DefaultInstaller playerInstaller = new DefaultInstaller();

        playerInstaller.Bind<IWeapon, RPG>(TypeParams.Instance);

        Player player = new Player();

        player.InitDependencies(playerInstaller);

        player.Shoot();
        
# Пример 2. Использование контейнера

        InstallContainer container = new(typeof(DefaultInstaller));

        container.Select<RandomValueLogger>()
            .Bind<ILogger, Logger>(TypeParams.Instance)
            .Bind<IRandomizer, Randimizer2>(TypeParams.Instance);

        RandomValueLogger? randomValueLogger = container?.Build<RandomValueLogger>();

        randomValueLogger.ShowRandomValue();
