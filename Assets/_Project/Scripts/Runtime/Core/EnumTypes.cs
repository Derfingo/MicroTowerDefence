public enum EnemyType
{
    SmallSlime,
    MediumSlime,
    LargeSlime,
    Wizard,
}

public enum ProjectileType
{
    Arrow,
    Shell,
    Explosion,
    Sphere,
    Test
}

public enum TowerType
{
    Beam,
    Mortar,
    Archer,
    Magic,
}

public enum ElementType : byte
{
    None = 0,
    Physical,
    Magic,
    Fire,
    Water,
    Ice,
    Electro,
    Dark,
    Holy
}

public enum MovementType
{
    Move,
    Lerp
}

public enum LayerType
{
    Ground,
    Enemy,
    Armor
}

public enum CameraType
{
    Isometric,
    Perspective
}