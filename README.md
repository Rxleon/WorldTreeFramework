# WorldTreeFramework
个人框架，始于2022.5.6

# 核心思路
- 1.框架由树状节点构成，无单例
- 2.通过反射自动注册的事件系统（为了解耦）

# 核心模块

### Node节点
- 框架树状的构成，思路为一切皆node，赖树状结构做到节点的从池的获取，和自动回收到池

### Id管理器
- 给节点分发唯一id

### 法则管理器（事件系统）
- 在启动时反射全局继承接口的法则类型，将类型自动注册到字典的事件系统
    - 支持泛型
    - 支持多态
    - 支持多播
    - 支持多参数（最多5个）
    - 支持返回值
    - 支持异步等待
    - 逆变强制约束

- 法则集合，可以对指定实例进行事件调用
- 法则执行器 （替代委托，因为事件系统不是通过委托方法注册实现的）



### 对象池
思路为将一切对象封成节点，所以对象池只在框架模块内调用。

- 单位对象池， 框架启动前用的对象池
- 数组对象池， 为了实现 0GC 的数组对象池
- 节点对象池， 框架启动后构成树状的节点对象池


### 监听器 
设计思路：可给指定类型提供类似Update这种默认的生命周期服务。
省去手动注册繁琐的同时，也避免了忘记解除绑定导致的异常。
- 静态监听器，监听指定类型节点的获取和回收
- 动态监听器，监听全局节点的获取和回收，但可任意更改监听目标类型

## 单线程异步 待改

---
# 功能模块
### 异步锁 
- 将一段代码变成异步队列执行，和多线程锁类似
### 数据绑定与数据监听 
- 单向绑定和双向绑定
- 不同类型的转换绑定 
### Tween渐变 实现中
### Touch触摸系统 未实现
- UI触摸事件扩展
- 物体触摸事件
### UI管理器 未实现
- UI栈的管理

---
# 其它功能
### C#实现的多层感知机



    
   
