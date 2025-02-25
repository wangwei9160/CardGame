using System;

[Serializable]
public class CardTplInfo : BaseTplInfo
{
    public string Name { get; set; }
    public string Description { get; set; }
}

[Serializable]
public class CardTpl : BaseTpl<CardTplInfo>
{

    protected override string TplName => "card";
}