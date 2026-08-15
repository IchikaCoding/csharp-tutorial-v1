using System;
using System.Collections.Generic;
using System.Text;

namespace FileTrainingV2
{
    public class Weapon
    {
        // TODO: Name、AttackPower ってreadonlyだけでいいのでは？
        public string Name { get; }
        public int AttackPower { get; }
        // これはユーザー側は参照だけでいいの？
        public readonly string SerialNumber;
        // ユーザーは参照のみ
        public readonly DateTime CreatedAt;
        // TODO: アクセス修飾子がわからなかった
        public int Durability { get; private set; }
        public Weapon(
            string name,
            int attackPower,
            string serialNumber,
            DateTime createdAt,
            int durability)
        {
            // TODO: nameof(serialNumber)をつけると気が利くかも！
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("名前を入力してください");
            }
            if (attackPower <= 0)
            {
                throw new ArgumentException("攻撃力は1以上を入力してください");
            }
            if (String.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("シリアルナンバーを入力してください。");
            }
            if (durability <= 0)
            {
                throw new ArgumentException("耐久値は1以上を入力してください");
            }
            Name = name;
            AttackPower = attackPower;
            SerialNumber = serialNumber;
            CreatedAt = createdAt;
            Durability = durability;
        }
        public void Use()
        {
            if (Durability <= 0)
            {
                throw new InvalidOperationException("この武器の耐久値が足りません！武器を変えてください。");
            }
            Durability--;
        }
    }
}
