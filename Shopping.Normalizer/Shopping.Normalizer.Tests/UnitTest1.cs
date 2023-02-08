namespace Shopping.Normalizer.Tests;

public class Tests
{

    [Test]
    public void Test_That_Line_Is_Converted_Into_An_Item_With_Quantity() // TODO: make better name.
    {
        var sample = new[]
            {
                "Paclan ������ ����������� ���������� Party Classic 200 �� 12��",
                "������",
                "12",
                "��"
            };

        ref string actualLine = ref sample[0];
        var (name, count, measure) = (sample[1], sample[2], sample[3]);

        ItemPosition result = new();

        Assert.That(result.Name, Is.EqualTo(name));
        Assert.That(result.Count, Is.EqualTo(count));
        Assert.That(result.Measure, Is.EqualTo(measure));
    }

    class Item
    {
        public string Name { get; set; }
    }

    class ItemPosition : Item
    {
        public string Count { get; set; }
        public string Measure { get; set; }
    }
}