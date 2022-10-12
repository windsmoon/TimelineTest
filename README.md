# TimelineTest

这是一篇文档，用来讲解 Timeline 的相关操作和代码方面的使用，因项目需要，unity 版本为 2022.1.7f1c1。

下面的视频是 demo 效果，最开始是通过 timeline 控制动画和镜头等，最后横板准备战斗的动作是用代码切换的，这之后就直接切换到代码控制了。

<video src="https://user-images.githubusercontent.com/29686189/195243121-3d402031-cdc6-4f4f-93e5-86a6e3fbeb68.mp4"></video>

# Timeline 的基本使用

**Timeline 的使用方法主要还是依赖于视频讲解和现场演示，这里只做简单介绍。**一些操作和大部分此类工具都差不多，不赘述了。下面的 timeline 界面内容就是上面视频中的效果。

![1](images/1.png)

上图是 Timeline 的大致界面：

+ 1 是该 Timeline 资源所有的 Track，每个 Track 可以有自己绑定的物体以实现对其的各种操作，也可以没有，具体看需求。各个 Track 干自己的事情，所以使用 Timeline 可以方便地制作复杂的过场动画。
+ 2 是 Timeline 编辑器的时间轴。
+ 3 是该 Timeline 当前走到的时间，可以用鼠标拖动来反复观看自己想要关注的片段。
+ 4 是该 Timeline 中 Cinemachine Track 的内容， 该 Track 中有几个 Clip，每个 Clip 中存储着该时间片段中 Cinemachine 相关的数据，这些数据被用来在这个时间段内对绑定的 Cinemachine 相关组件进行设置。
+ 5 中的白色标志属于该 Timeline 中的 Signal Track，叫做 Signal Emitter，它专门用来在特定时间点抛出特定信号/事件，这样监听它的代码就可以执行对应的操作，Signal Emitter 是一个可以复用的资源。
+ 6 是 当前选中的 Clip 的 Inspector 面板，跟 MonoBehaviour 的基本一致，另外 Track 本身也有 Inspector 面板。
+ 7 是设置界面，可以设置时间轴用时间还是帧数来表示以及一些其他东西。

在方框 1 中右键或者点击上面的加号按钮，可以添加各种内置和自定义的 Track，然后在 Track 中邮件可以添加对应的 Clip，然后就可以设置值了。用鼠标可以移动和缩放 Clip，当多个 Clip 重叠时，可能会出现混合器（Mixer）的相关设置，用户可以设置想要的过渡曲线，或者直接用默认的就行。**注意不是所有 Track 都有 Mixer，这个要看需求，比如 Activation Track 只控制物体的开关，这个操作只有开关两个状态，不可能有中间状态，也就不可能有 Mixer。**

## 内置和官方额外提供的 Track 介绍

官方提供了几个 Track，点击加号按钮如下图所示：

![2](images/2.png)

第一行 Track Group 是创建一个 Track 组，就相当于文件夹，用处就是分类，方便管理。

第一个横线和第二个横线之间的是 Unity 内置的几个 Track：

+ Activation Track ：用来控制物体的禁用启用状态
+ Animation Track ：通过 GameObject 上的 Animator 组件来播放动画，**注意，如果物体身上没有 Animator 则会自动创建一个，且该 Track 只是利用了 Animator 的设置来播放 Track 上的 Clip 中设置的动画，并不会暂停 Animator 本身的执行。**
+ Audio Track ：控制声音的 Track。
+ Control Track ：对多个物体进行一些特定行为的控制，比如 Particle System 等，**注意该 Track 没有绑定的物体，直接在具体的 Clip 中设置具体的物体。**
+ Signal Track ：在特定时间点发送 Signal，用来和外界交互。
+ Playable Track ：自定义行为的 Track，看需求，也可以用自己写的自定义 Track 来代替。

后面的都是自定义 Track，第一个是 Cinemachine 自带的用来控制镜头的 Track，第二个是 Unity 录像插件自带的，最后一个是我自己写的测试用 Track，其余都是官方在资源商店提供的额外 Track，前往 Asset Store 上搜 DefaultPlayables 然后下载就可以：

+ Cinemachine Track ：控制镜头的 Track，也是和 Timeline 一起使用最多的 Track。
+ Light Control Track ：控制光照的 Track。
+ Nav Mesh Agent Control Track ：控制寻路的 Track。
+ Screen Fader Track ：用来做全屏淡入淡出的效果。
+ Text Switcher Track ：用来切换文字。
+ Time Dilation Track ：用来控制时间缩放。
+ Transform Tween Track ：用来控制物体的一些变换动画。
+ Video Script Playable Track ：用来控制视频播放。

其余的没必要介绍了。

### Cinemachine Track 使用举例

首先创建两个空物体，一个叫 Look，一个叫 Follow，都加上 CinemachineVirtualCamera 组件。在主相机上加上 CinemachineBrain 组件。

Look 物体上的虚拟相机的作用是在固定点盯着某个东西，所以我们这个物体的位置挪到观察点，并且设置观察目标：

![3](images/3.png)

上图中 Look At 变量里的 LookPoint 就是观察目标，当启用这个虚拟相机时，主相机上的 CinemachineBrain 组件就会将主相机的参数位置等数据设置为该虚拟相机的数据，并且一直在观察点看着观察目标。当观察目标移动时，相机也会旋转以保证一直看着观察目标（但不会移动位置）。

Follow 物体上的虚拟相机的作用是，相机会跟随目标点，并保持一定的距离，所以当目标点走远时，相机也会移动位置跟上目标点：

![4](images/4.png)

设置好 cinemachine 后，在 Timeline 中创建 Cinemachine Track，将主摄像机物体拖到其绑定变量上（因为这个 Track 需要绑定 CinemachineBrain），然后将两个有虚拟相机组件的物体拖到 Track 上，如下图所示

![5](images/5.png)

当该 Timeline 开始运行时，整个 Track 完全由 Look 虚拟相机控制，这时，Track 上绑定的 CinemachineBrain 会启用 Look 虚拟相机的设置，表现就是游戏中主相机会在 Look 虚拟相机的位置看着 LookPoint。而当进入到中间浅色部分时，Look Clip 并没有结束，但是 Follow 已经开始了，这时两个 Clip 重叠，Cinemachine Track 会让这两个虚拟相机进行插值，表现上就是随着时间的进行，相机逐渐从 Look 虚拟相机的位置平滑移动到设置好的 Follow 相机经过计算应该出现的位置，之后 Look Clip 退出，结果完全交由 Follow 虚拟相机控制。这就是上面视频演示时镜头的行为，最后切换到横板视角时这里就不赘述了。

**注意，当整个 Timeline 结束时，不同 Track 可能有不同的行为，Cinemachine Track 的行为是回到优先级最高的虚拟相机（当没有做任何处理时，CinemachineBrain 会选择激活优先级最高的虚拟相机），并且退出 Timeline 时也可以进行插值设置。**



# 自定义 Track

当内置的 Track 无法满足我们的需求是，就需要自己定义 Track 和一系列相关代码来实现需求。

我们这里以在 Timeline 中控制后处理的 Vignette 效果的 Intensity 参数为例，自定义 Track 一般需要实现 4 个类：

+ VignetteControlTrack ：该类需要继承自 TrackAsset，这允许我们在 Timeline 创建 Track 列表中找到这个类。
+ VigentteControlClip ：该类需要继承自 PlayableAsset，这允许我们创建 Clip 到轨道上，一般还需要实现 ITimelineClipAsset 接口，这会让 Inspector 面板去显示一些相关的设置。
+ VignetteControlBehaviour ：该类需要继承自 PlayableBehaviour，表示 Clip 自己的行为，通过回调来执行具体的逻辑。
+ VignetteControlMixerBehaviour ：该类和 VignetteControlBehaviour  一样需要继承自 PlayableBehaviour，表示当多个 Clip 混合时的行为，但没有混合时，会变成只有某一个 Clip 或者一个都没有情况来混合，所以有了这个一般就不要再在 VignetteControlBehaviour 里执行同样的回调了，但依然要写它，因为它此时会作为一个数据类来使用。

VignetteControlTrack 类代码如下：

``` C#
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    [TrackBindingType(typeof(Volume))]
    [TrackClipType(typeof(VignetteControlClip))]
    [TrackColor(0.32f, 0.21f,0.55f)]
    [Serializable]
    public class VignetteControlTrack : TrackAsset
    {
        #region methods
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<VignetteControlMixerBehaviour>.Create(graph, inputCount);
        }
        #endregion
    }
}
```

TrackBindingType 表示这个 Track 绑定的是什么类型的对象，这个对象限制跟 Monobehaviour 可以拖拽的对象一样，这里我们要操作的是后处理中的 Vignette 效果，因为 Vignette 本身不是一个 Component，所以不能直接绑定，我们要绑定后处理组件，即 Volume 组件。**注意这个也可以没有，表示不绑定任何东西。**

TrackClipType 表示我们可以在这个轨道上创建哪种类型的 Clip，自然是我们自定义的 VignetteControlClip。

TrackColor 表示在 Timeline 界面这个 Track 要显示什么颜色，也可以不设置。

CreateTrackMixer 函数表示创建混合器，他将接管整个 Timeline 过程中的这个 Track 的行为，代码返回的是一个 Playable 对象，通过

``` C#
ScriptPlayable<VignetteControlMixerBehaviour>.Create(graph, inputCount);
```

来创建。```ScriptPlayable<T>``` 继承自 Playable，可以先不用管 Playable 是什么，把他理解为一个图中的节点就可以，这行代码也会创建一个 VignetteControlMixerBehaviour 实例存在这个 Playable 对象里。我们先关注这几个类之间的关系，就可以先学会怎么自定义 Track 并且使用它了。 因为这里是创建一个 Mixer，所以泛型参数是 VignetteControlMixerBehaviour。

VignetteControlClip 类代码如下：

``` C#
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    [Serializable]
    public class VignetteControlClip : PlayableAsset, ITimelineClipAsset
    {
        #region fields
        [SerializeField, Range(0f, 1f)]
        private float intensity;
        #endregion
        
        #region properties
        public ClipCaps clipCaps
        {
            get => ClipCaps.All;
        }
        #endregion

        #region methods
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<VignetteControlBehaviour>.Create(graph);
            VignetteControlBehaviour volumeControlBehaviour = playable.GetBehaviour();
            volumeControlBehaviour.Intensity = intensity; 
            return playable;
        }
        #endregion
    }
}
```

VignetteControlClip 类代码如下：

``` C#
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    [Serializable]
    public class VignetteControlClip : PlayableAsset, ITimelineClipAsset
    {
        #region fields
        [SerializeField, Range(0f, 1f)]
        private float intensity;
        #endregion
        
        #region properties
        public ClipCaps clipCaps
        {
            get => ClipCaps.All;
        }
        #endregion

        #region methods
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<VignetteControlBehaviour>.Create(graph);
            VignetteControlBehaviour volumeControlBehaviour = playable.GetBehaviour();
            volumeControlBehaviour.Intensity = intensity; 
            return playable;
        }
        #endregion
    }
}
```

VignetteControlClip 是 VignetteControlTrack 中的内容，点击后可以在 Inspector 面板上设置参数：

这里的规则和 MonoBehaviour 的 Inspector 面板一样，Unity 会自动序列化和暴露 public 的字段，但你也可以选择设置字段为 private，并且给其打上  [SerializeField] 这个 Attribute，这样更符合 C# 的语法习惯。如果是个 float 字段，则可以加上 [Range(0f, 1f)] 这个 Attribute，表示这个字段的值的范围是 0 到 1，这样你在面板上就可以通过一个滑块来控制这个值，范围也可以设置成你想要的任何值。

Vignette 效果中的 Intensity 是你要用 Timeline 控制的属性，所以我们需要在 VignetteControlClip 的 Inspector 面板中暴露一个变量，用来填入你想设置的值。所以这里的代码中要定义一个 intensity 字段，并且设置上 [SerializeField, Range(0f, 1f)]。

clipCaps 是因为实现了 ITimelineClipAsset 接口，这个用来在 Inspector 面板上展示一些内置的设置，一般就返回 ClipCaps.All 这个枚举就行。

有了 intensity 和 clipCaps，现在点击一个 VignetteControlClip，右侧 Inspector 面板上会这样显示：

![6](images/6.png)

Vignette Control Clip 这条线上面的都是通过 clipCaps 自动显示的，下面则是自己定义的要暴露的字段，可以看到这个变量因为我们设置了 [Range(0f, 1f)] 从而直接变成了一个滑块。

CreatePlayable 函数也是返回一个 Playable，但 Clip 函数中没有 Mixer 相关的操作，所以我们泛型参数传的是 VignetteControlBehaviour。但和创建 Mixer 不一样的是，我们要通过创建出来的 Playable 对象去取到其中的 VignetteControlBehaviour，然后将 intensity 字段的值传进去，后面会讲为什么这么做。

VignetteControlBehaviour 类代码如下：

``` C#
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    public class VignetteControlBehaviour : PlayableBehaviour
    {
        #region fields
        private float intensity;
        #endregion

        #region properties
        public float Intensity
        {
            get => intensity;
            set => intensity = value;
        }
        #endregion

        #region methods
        // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        // {
        //     Volume volume = playerData as Volume;
        //
        //     if (volume.profile.TryGet<Vignette>(out Vignette vignette))
        //     {
        //         vignette.intensity.SetValue(new ClampedFloatParameter(intensity, 0.0f, 1f, true));
        //     }
        //     
        //     base.ProcessFrame(playable, info, playerData);
        // }
        #endregion
    }
}
```

PlayableBehaviour 顾名思义，在这里我们要执行具体的控制逻辑，这里有个 intensity 字段，我们在之前的 VignetteControlClip 中对其进行了赋值，然后我们就可以把这个值真正的传给后处理组件了。先忽略注释，我们看这个函数，ProcessFrame，我们要在这个类里重写这个方法，该方法在 Timeline 执行到其对应的 Clip 的时候会每帧都执行，不在这个 Clip 的时候就不会执行。

在这个函数里，我们显然要获取后处理组件的引用，函数参数里的 playerData，就是 Track 上绑定的对象，所以将其转为 Volume 后，就可以获取 Vignette，然后修改其数据。**注意，修改 Volume 中的数据一定要用 SetValue，不要直接赋值，否则你会发现这时你再在后处理组件的面板上去改这个值就没效果了，必须把 Volume 的 profile 保存一下才可以，这对于开发来说是很麻烦的。**

但我这里给注释掉了，因为我们有 Mixer，这个 Track 就不要在 VignetteControlBehaviour 中执行操作了。

VignetteControlMixerBehaviour 类的代码如下：

``` C#
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameLogic.Timeline.PlayableExtensions.VignetteControl
{
    public class VignetteControlMixerBehaviour : PlayableBehaviour
    {
        #region fields
        private bool isFirstFrame = true;
        private float oldIntensity;
        #endregion
        
        #region mehtods
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Volume volume = playerData as Volume;

            if (volume.profile.TryGet<Vignette>(out Vignette vignette) == false)
            {
                base.ProcessFrame(playable, info, playerData);
                return;
            }
            
            if (isFirstFrame)
            {
                isFirstFrame = false;
                oldIntensity = vignette.intensity.GetValue<float>();
            }
            
            int inputCount = playable.GetInputCount();
            float totalWeight = 0f;
            float blendedIntensity = 0f;
            
            for (int i = 0; i < inputCount; i++)
            {
                float weight = playable.GetInputWeight(i);
                ScriptPlayable<VignetteControlBehaviour> inputPlayable = (ScriptPlayable<VignetteControlBehaviour>)playable.GetInput(i);
                VignetteControlBehaviour volumeControlBehaviour = inputPlayable.GetBehaviour();
                blendedIntensity += volumeControlBehaviour.Intensity * weight;
                totalWeight += weight;
            }
            
            vignette.intensity.SetValue(new ClampedFloatParameter(Mathf.Lerp(oldIntensity, blendedIntensity, totalWeight), 0.0f, 1f, true));
            base.ProcessFrame(playable, info, playerData);
        }
        #endregion
    }
}
```

这个类其实不需要任何字段，但是为了实现一些需求，我还是加了两个字段，一个是 isFirstFrame，表示是否是第一帧，第二个是 oldIntensity，这个其实就是把没有 Timeline 时的状态值记录了下来，以方便退出 Timeline 后还原，当然，你也可以选择不还原，这个要看具体需求。我们会在 ProcessFrame（MixerBehaviour 也是继承自 PlayableBehaviour）中判断是否是第一帧，是就把初始状态值记录下来。

后面就是正常的混合逻辑了，虽然我们不用 VignetteControlBehaviour 来处理逻辑，但我们需要获取它来得到他们的值，然后才能混合。这里捋一下逻辑，每个 VignetteControlClip 会创建一个 VignetteControlBehaviour，而你要设置的数据是通过 VignetteControlClip 的 Inspector 面板设置的，我们在 VignetteControlClip 中把设置的值传递给了 VignetteControlBehaviour 对象，而在 VignetteControlMixerBehaviour 中，就要获取所有参与混合的 VignetteControlBehaviour，得到其中的数据，并通过权重混合出一个结果。

