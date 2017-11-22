﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Enum.Attribute;

namespace ERP.Enum
{
    /// <summary>
    /// 储存条件
    /// </summary>
    public enum MedicineStorageConditionType
    {
        /// <summary>
        /// 10～30℃
        /// </summary>
        [Enum("10～30℃")]
        十到三十摄氏度 = 0,

        /// <summary>
        /// 常温,30℃以下
        /// </summary>
        [Enum("常温,30℃以下")]
        常温三十摄氏度以下 = 1,

        /// <summary>
        /// 常温,密闭,遮光,10～30℃
        /// </summary>
        [Enum("常温,密闭,遮光,10～30℃")]
        常温密闭遮光十到三十摄氏度 = 2,

        /// <summary>
        /// 避光,常温,密封,10～30℃
        /// </summary>
        [Enum("避光,常温,密封,10～30℃")]
        避光常温密封十到三十摄氏度 = 3,
        /// <summary>
        /// 在阴凉（不超过20℃）干燥处保存,防蛀
        /// </summary>
        [Enum("在阴凉（不超过20℃）干燥处保存,防蛀")]
        在阴凉不超过二十摄氏度干燥处保存防蛀 = 4,

        /// <summary>
        /// 密封,避光,密封，干燥处保存
        /// </summary>
        [Enum("密封,避光,密封，干燥处保存")]
        密封避光密封干燥处保存 = 5,

        /// <summary>
        /// 避光,常温,密闭,10～30℃
        /// </summary>
        [Enum("避光,常温,密闭,10～30℃")]
        避光常温密闭十到三十摄氏度 = 6,

        /// <summary>
        /// 遮光,密封保存
        /// </summary>
        [Enum("遮光,密封保存")]
        遮光密封保存 = 7,
        /// <summary>
        /// 密闭,在阴凉（不超过20℃）干燥处保存,防蛀
        /// </summary>
        [Enum("密闭,在阴凉（不超过20℃）干燥处保存,防蛀")]
        密闭在阴凉不超过二十摄氏度干燥处保存防蛀 = 8,

        /// <summary>
        /// 避光,密闭，在凉暗干燥处保存
        /// </summary>
        [Enum("避光,密闭，在凉暗干燥处保存")]
        避光密闭在凉暗干燥处保存 = 9,

        /// <summary>
        /// 阴凉,在阴凉处保存（不超过20℃）
        /// </summary>
        [Enum("阴凉,在阴凉处保存（不超过20℃）")]
        阴凉在阴凉处保存不超过二十摄氏度 = 10,
        /// <summary>
        /// 干燥,密封,15℃～25℃
        /// </summary>
        [Enum("干燥,密封,15℃～25℃")]
        干燥密封1十五摄氏度到二十五摄氏度 = 11,

        /// <summary>
        /// 密闭,遮光
        /// </summary>
        [Enum("密闭,遮光")]
        密闭遮光 = 12,

        /// <summary>
        /// 密闭,凉暗处（避光并不超过20℃)保存
        /// </summary>
        [Enum("密闭,凉暗处（避光并不超过20℃)保存")]
        密闭凉暗处避光并不超过二十摄氏度保存 = 13,
        /// <summary>
        /// 避光,密闭,防潮,10～30℃
        /// </summary>
        [Enum("避光,密闭,防潮,10～30℃")]
        避光密闭防潮十到三十摄氏度 = 14,

        /// <summary>
        /// 特殊温度15℃-30℃
        /// </summary>
        [Enum("特殊温度15℃-30℃")]
        特殊温度十五摄氏度到三十摄氏度 = 15,

        /// <summary>
        /// 常温,密封,30℃以下
        /// </summary>
        [Enum("常温,密封,30℃以下")]
        常温密封三十摄氏度以下 = 16,

        /// <summary>
        /// 在阴凉处保存（不超过20℃）,通风、干燥
        /// </summary>
        [Enum("在阴凉处保存（不超过20℃）,通风、干燥")]
        在阴凉处保存不超过二十摄氏度通风干燥 = 17,
        /// <summary>
        /// 通风、干燥
        /// </summary>
        [Enum("通风、干燥")]
        通风干燥 = 18,

        /// <summary>
        /// 阴凉,10～30℃
        /// </summary>
        [Enum("阴凉,10～30℃")]
        阴凉时到三十摄氏度 = 19,

        /// <summary>
        /// 密封
        /// </summary>
        [Enum("密封")]
        密封 = 20,

        /// <summary>
        /// 2℃-8℃保存
        /// </summary>
        [Enum("2℃-8℃保存")]
        二摄氏度到八摄氏度保存 = 21,
        /// <summary>
        /// 凉暗,(不超过20℃)
        /// </summary>
        [Enum("凉暗,(不超过20℃)")]
        凉暗不超过二十摄氏度 = 22,

        /// <summary>
        /// 25℃以下
        /// </summary>
        [Enum("25℃以下")]
        二十五摄氏度以下 = 23,

        /// <summary>
        /// 密封,干燥
        /// </summary>
        [Enum("密封,干燥")]
        密封干燥 = 24,
        /// <summary>
        /// 15℃～25℃
        /// </summary>
        [Enum("15℃～25℃")]
        十五摄氏度到二十五摄氏度 = 25,

        /// <summary>
        /// 2～10℃
        /// </summary>
        [Enum("2～10℃")]
        二到十摄氏度 = 26,

        /// <summary>
        /// 常温(10℃-30℃)
        /// </summary>
        [Enum("常温(10℃-30℃)")]
        常温十摄氏度到三十摄氏度 = 27,
        /// <summary>
        /// 避光,常温(10℃-30℃)
        /// </summary>
        [Enum("避光,常温(10℃-30℃)")]
        避光常温十摄氏度到三十摄氏度 = 28,

        /// <summary>
        /// 密闭,遮光,在阴凉处保存（不超过20℃）
        /// </summary>
        [Enum("密闭,遮光,在阴凉处保存（不超过20℃）")]
        密闭遮光在阴凉处保存不超过二十摄氏度 = 29,

        /// <summary>
        /// 阴凉,密封,(不超过20℃)
        /// </summary>
        [Enum("阴凉,密封,(不超过20℃)")]
        阴凉密封不超过二十摄氏度 = 30,

        /// <summary>
        /// 密闭,遮光,在30℃以下保存
        /// </summary>
        [Enum("密闭,遮光,在30℃以下保存")]
        密闭遮光在三十摄氏度以下保存 = 31,
        /// <summary>
        /// 常温,在30℃以下保存
        /// </summary>
        [Enum("常温,在30℃以下保存")]
        常温在三十摄氏度以下保存 = 32,

        /// <summary>
        /// 密封,30℃以下
        /// </summary>
        [Enum("密封,30℃以下")]
        密封三十摄氏度以下 = 33,

        /// <summary>
        /// 密闭,在阴凉（不超过20℃）干燥处保存
        /// </summary>
        [Enum("密闭,在阴凉（不超过20℃）干燥处保存")]
        密闭在阴凉不超过二十摄氏度干燥处保存 = 34,

        /// <summary>
        /// 常温
        /// </summary>
        [Enum("常温")]
        常温 = 35,
        /// <summary>
        /// 常温,10～30℃
        /// </summary>
        [Enum("常温,10～30℃")]
        常温十到三十摄氏度 = 36,

        /// <summary>
        /// 避光,阴凉,(不超过20℃)
        /// </summary>
        [Enum("避光,阴凉,(不超过20℃)")]
        避光阴凉不超过二十摄氏度 = 37,

        /// <summary>
        /// 密闭,阴凉,(不超过20℃)
        /// </summary>
        [Enum("密闭,阴凉,(不超过20℃)")]
        密闭阴凉不超过二十摄氏度 = 38,
        /// <summary>
        /// 干燥,25℃以下
        /// </summary>
        [Enum("干燥,25℃以下")]
        干燥二十五摄氏度以下 = 39,

        /// <summary>
        /// 干燥,避光,阴凉,密封,(不超过20℃)
        /// </summary>
        [Enum("干燥,避光,阴凉,密封,(不超过20℃)")]
        干燥避光阴凉密封不超过二十摄氏度 = 40,

        /// <summary>
        /// 干燥
        /// </summary>
        [Enum("干燥")]
        干燥 = 41,
        /// <summary>
        /// 干燥,密闭,阴凉,遮光,(不超过20℃)
        /// </summary>
        [Enum("干燥,密闭,阴凉,遮光,(不超过20℃)")]
        干燥密闭阴凉遮光不超过二十摄氏度 = 42,

        /// <summary>
        /// 密闭
        /// </summary>
        [Enum("密闭")]
        密闭 = 43,

        /// <summary>
        /// 干燥,避光,密闭,30℃以下
        /// </summary>
        [Enum("干燥,避光,密闭,30℃以下")]
        干燥避光密闭三十摄氏度以下 = 44,

        /// <summary>
        /// 密闭,常温
        /// </summary>
        [Enum("密闭,常温")]
        密闭常温 = 45,
        /// <summary>
        /// 干燥,密闭,阴凉,(不超过20℃)
        /// </summary>
        [Enum("干燥,密闭,阴凉,(不超过20℃)")]
        干燥密闭阴凉不超过二十摄氏度 = 46,

        /// <summary>
        /// 密闭,密封，干燥处保存
        /// </summary>
        [Enum("密闭,密封，干燥处保存")]
        密闭密封干燥处保存 = 47,

        /// <summary>
        /// 干燥,阴凉,密封,(不超过20℃)
        /// </summary>
        [Enum("干燥,阴凉,密封,(不超过20℃)")]
        干燥阴凉密封不超过二十摄氏度 = 48,

        /// <summary>
        /// 阴凉
        /// </summary>
        [Enum("阴凉")]
        阴凉 = 49,
        /// <summary>
        /// 常温,遮光,密封,防潮,10～30℃
        /// </summary>
        [Enum("常温,遮光,密封,防潮,10～30℃")]
        常温遮光密封防潮十到三十摄氏度 = 50,

        /// <summary>
        /// 避光,密闭,阴凉,(不超过20℃)
        /// </summary>
        [Enum("避光,密闭,阴凉,(不超过20℃)")]
        避光密闭阴凉不超过二十摄氏度 = 51,

        /// <summary>
        /// 密封,常温(10℃-30℃)
        /// </summary>
        [Enum("密封,常温(10℃-30℃)")]
        密封常温十摄氏度到三十摄氏度 = 52,
        /// <summary>
        /// 常温,遮光,密封,10～30℃
        /// </summary>
        [Enum("常温,遮光,密封,10～30℃")]
        常温遮光密封十到三十摄氏度 = 53,

        /// <summary>
        /// 干燥,常温,密封,10～30℃
        /// </summary>
        [Enum("干燥,常温,密封,10～30℃")]
        干燥常温密封十到三十摄氏度 = 54,

        /// <summary>
        /// (不超过20℃)
        /// </summary>
        [Enum("(不超过20℃)")]
        不超过二十摄氏度 = 55,
        /// <summary>
        /// 密闭,阴凉,遮光,(不超过20℃)
        /// </summary>
        [Enum("密闭,阴凉,遮光,(不超过20℃)")]
        密闭阴凉遮光不超过二十摄氏度 = 56,

        /// <summary>
        /// 干燥,阴凉,(不超过20℃)
        /// </summary>
        [Enum("干燥,阴凉,(不超过20℃)")]
        干燥阴凉不超过二十摄氏度 = 57,

        /// <summary>
        /// 密闭，在凉暗处保存(不超过20℃)
        /// </summary>
        [Enum("密闭，在凉暗处保存(不超过20℃)")]
        密闭在凉暗处保存不超过二十摄氏度 = 58,
        /// <summary>
        /// 避光,2～8℃
        /// </summary>
        [Enum("避光,2～8℃")]
        避光二到八摄氏度 = 59,

        /// <summary>
        /// 常温,(不超过20℃)
        /// </summary>
        [Enum("常温,(不超过20℃)")]
        常温不超过二十摄氏度 = 60,

        /// <summary>
        /// 常温,密闭,30℃以下
        /// </summary>
        [Enum("常温,密闭,30℃以下")]
        常温密闭三十摄氏度以下 = 61,
        /// <summary>
        /// 避光,10-25℃
        /// </summary>
        [Enum("避光,10-25℃")]
        避光是到二十五摄氏度 = 62,

        /// <summary>
        /// 阴凉,(不超过20℃)
        /// </summary>
        [Enum("阴凉,(不超过20℃)")]
        阴凉不超过二十摄氏度 = 63,

        /// <summary>
        /// 密闭,遮光,在阴凉（不超过20℃）干燥处保存
        /// </summary>
        [Enum("密闭,遮光,在阴凉（不超过20℃）干燥处保存")]
        密闭遮光在阴凉不超过二十摄氏度干燥处保存 = 64,
        /// <summary>
        /// 密封,遮光,在阴凉（不超过20℃）干燥处保存
        /// </summary>
        [Enum("密封,遮光,在阴凉（不超过20℃）干燥处保存")]
        密封遮光在阴凉不超过二十摄氏度干燥处保存 = 65,

        /// <summary>
        /// 阴凉,25℃以下
        /// </summary>
        [Enum("阴凉,25℃以下")]
        阴凉二十五摄氏度以下 = 66,

        /// <summary>
        /// 常温,密闭,10～30℃
        /// </summary>
        [Enum("常温,密闭,10～30℃")]
        常温密闭十到三十摄氏度 = 67,
        /// <summary>
        /// 密封,避光
        /// </summary>
        [Enum("密封,避光)")]
        密封避光 = 68,

        /// <summary>
        /// 密闭,常温(10℃-30℃)
        /// </summary>
        [Enum("密闭,常温(10℃-30℃)")]
        密闭常温十摄氏度到三十摄氏度 = 69,

        /// <summary>
        /// 干燥,避光,阴凉,(不超过20℃)
        /// </summary>
        [Enum("干燥,避光,阴凉,(不超过20℃)")]
        干燥避光阴凉不超过二十摄氏度 = 70,
        /// <summary>
        /// 密封保存
        /// </summary>
        [Enum("密封保存")]
        密封保存 = 71,

        /// <summary>
        /// 冷冻
        /// </summary>
        [Enum("冷冻")]
        冷冻 = 72,

        /// <summary>
        /// 干燥,常温,遮光,密封,10～30℃
        /// </summary>
        [Enum("干燥,常温,遮光,密封,10～30℃")]
        干燥常温遮光密封十到三十摄氏度 = 73,
        /// <summary>
        /// 凉暗,密封,(不超过20℃)
        /// </summary>
        [Enum("凉暗,密封,(不超过20℃)")]
        凉暗密封不超过二十摄氏度 = 74,

        /// <summary>
        /// 常温,密封,10～30℃
        /// </summary>
        [Enum("常温,密封,10～30℃")]
        常温密封十到三十摄氏度 = 75,

        /// <summary>
        /// 密封,在阴凉处保存（不超过20℃）
        /// </summary>
        [Enum("密封,在阴凉处保存（不超过20℃）")]
        密封在阴凉处保存不超过二十摄氏度 = 76,
        /// <summary>
        /// 避光,阴凉,密封,(不超过20℃)
        /// </summary>
        [Enum("避光,阴凉,密封,(不超过20℃)")]
        避光阴凉密封不超过二十摄氏度 = 77,

        /// <summary>
        /// 密封,在阴凉（不超过20℃）干燥处保存
        /// </summary>
        [Enum("密封,在阴凉（不超过20℃）干燥处保存")]
        密封在阴凉不超过二十摄氏度干燥处保存 = 78,

        /// <summary>
        /// 在阴凉处保存（不超过20℃）
        /// </summary>
        [Enum("在阴凉处保存（不超过20℃）")]
        在阴凉处保存不超过二十摄氏度 = 79,

        /// <summary>
        /// 2～8℃
        /// </summary>
        [Enum("2～8℃")]
        二到八摄氏度 = 80,
        /// <summary>
        /// 在阴凉（不超过20℃）干燥处保存
        /// </summary>
        [Enum("在阴凉（不超过20℃）干燥处保存")]
        在阴凉不超过二十摄氏度干燥处保存 = 81,

        /// <summary>
        /// 干燥,防潮,通风
        /// </summary>
        [Enum("干燥,防潮,通风")]
        干燥防潮通风 = 82,

        /// <summary>
        /// 干燥,常温,密闭,阴凉,10～30℃
        /// </summary>
        [Enum("干燥,常温,密闭,阴凉,10～30℃")]
        干燥常温密闭阴凉十到三十摄氏度 = 83
    }
}