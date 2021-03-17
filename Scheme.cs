namespace SchemeEditor
{
    public class Scheme
    {
        private Block _mainBlock;
        private SchemeSettings _settings;
        
        // Список битмапов
        // Список graphics для рисования по битмапам
        // Сделать получение битмапов и их создание
        
        // 1) Расчёт координат для всех блоков с учётом переноса на другие страницы
        // 2) Сделать отрисовку каждого битмапа

        public Scheme(SchemeSettings settings)
        {
            _mainBlock = new Block(BlockType.Main, new[] {""}, new string[1]);
            _settings = settings;

            // Добавление блока в схему
            Block someBlock = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            _mainBlock.AddChild(someBlock, 0, 0);
        }
    }
}