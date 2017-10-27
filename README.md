DeskBand
--------------
这是使用Visula Basic.NET做的IDeskband2的实现

### 截图

![image](https://github.com/1102163785/DeskBandTest/raw/master/DeskBandTest/Images/ScreenShot.PNG)
![image](https://github.com/1102163785/DeskBandTest/raw/master/DeskBandTest/Images/UIDesigner.PNG)

### 遇到的问题与经验总结

>.NET定义的COM接口似乎不支持继承，有继承关系的COM接口在.NET代码中定义时无需继承，但是函数顺序、参数、返回必须正确无误，数据封送需要处理恰当。

>deskband组件界面似乎不支持.NET中定义的透明色，反倒是把背景色设置成黑色才会显现透明效果，不知道是Explorer的bug还是微软有意为之，所以上面截图设计界面背景是黑的。

>64位系统需要使用 *C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe* 这个程序以管理员权限来注册。
