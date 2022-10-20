using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.Control
{
    /// <summary>
    /// 歌曲详情,展示正在播放音乐的歌词等信息 的交互逻辑
    /// </summary>
    public partial class SongDetailControl : UserControl
    {
        public SongDetailControl()
        {
            InitializeComponent();


            List<LyricItem> list = new List<LyricItem>();

            list.Add(new LyricItem("作词: 乔与"));
            list.Add(new LyricItem("作曲 : 楚明玉"));
            list.Add(new LyricItem("编曲：曾吴秋杰"));

            list.Add(new LyricItem("我害怕沉默 就找话题"));

            list.Add(new LyricItem("每一句都慎重的考虑"));

            list.Add(new LyricItem("心酸和眼红我克制住 匿藏在心里"));

            list.Add(new LyricItem("我全力以赴你的情绪"));

            list.Add(new LyricItem("放弃了思考这种能力"));

            list.Add(new LyricItem("越爱越担心 失去"));

            list.Add(new LyricItem("我会治愈我不算为难"));

            list.Add(new LyricItem("温暖你变成了习惯"));

            list.Add(new LyricItem("你出示条件 我照单成全"));

            list.Add(new LyricItem("我纵容 你一寸 又一寸 将我的心 贯穿"));

            list.Add(new LyricItem("又一分 又一分 拔出来 一半"));

            list.Add(new LyricItem("让我能保持痛感"));

            list.Add(new LyricItem("迁就 你一次 又一次 分寸的 试探"));

            list.Add(new LyricItem("我一遍 又一遍 来配合 你出演"));

            list.Add(new LyricItem("我出众的情愿"));

            list.Add(new LyricItem("我害怕沉默 就找话题"));

            list.Add(new LyricItem("每一句都慎重的考虑"));

            list.Add(new LyricItem("心酸和眼红我克制住 匿藏在心里"));

            list.Add(new LyricItem("我全力以赴你的情绪"));

            list.Add(new LyricItem("放弃了思考这种能力"));

            list.Add(new LyricItem("越爱越担心 失去"));

            list.Add(new LyricItem("我会治愈我不算为难"));

            list.Add(new LyricItem("温暖你变成了习惯"));

            list.Add(new LyricItem("你出示条件 我照单成全"));

            list.Add(new LyricItem("我纵容 你一寸 又一寸 将我的心 贯穿"));

            list.Add(new LyricItem("又一分 又一分 拔出来 一半"));

            list.Add(new LyricItem("让我能保持痛感"));

            list.Add(new LyricItem("迁就 你一次 又一次 分寸的 试探"));

            list.Add(new LyricItem("我一遍 又一遍 来配合 你出演"));

            list.Add(new LyricItem("我出众的情愿"));

            list.Add(new LyricItem("我纵容 你一寸 又一寸 将我的心 贯穿"));

            list.Add(new LyricItem("又一分 又一分 拔出来 一半"));

            list.Add(new LyricItem("让我能保持痛感"));

            list.Add(new LyricItem("迁就 你一次 又一次 分寸的 试探"));

            list.Add(new LyricItem("我一遍 又一遍 来配合 你出演"));

            list.Add(new LyricItem("我出众的情愿"));

            list.Add(new LyricItem("制作人：曾吴秋杰"));

            list.Add(new LyricItem("混音：周星宇 / 曾吴秋杰"));

            list.Add(new LyricItem("吉他：曾吴秋杰"));

            list.Add(new LyricItem("和声：曾吴秋杰"));

            list.Add(new LyricItem("统筹：火蓉 / 季秋洋 / 黄鲲"));

            list.Add(new LyricItem("企划：丁柏昕 / 倪奇"));

            list.Add(new LyricItem("OP：深声文化"));

            list.Add(new LyricItem("出品：网易青云LAB x 网易飓风"));

            LyricList.ItemsSource = list;
        }
    }


    public class LyricItem
    {
        public LyricItem(string sentence)
        {
            this.sentence = sentence;
        }
        public string sentence { get; set; }
    }
}


