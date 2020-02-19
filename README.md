1. 克隆项目到你的本地文件夹中(`cores`)
```ssh
  git clone https://github.com/yourname/project.git cores
```

2. 进到`cores`目录下，然后增加远程分支，名为`Yd`到本地
```ssh
  git remote add Yd https://github.com/onetcore/webapi.git
```

3. 运行命令：`git remote -v`, 会发现多出来了一个`Yd`的远程分支
```ssh
  git remote -v
```

4. 然后把远程原始分支`Yd`的代码拉到本地  
```ssh
  git fetch Yd
```

5. 合并对方远程原始分支`Yd`的代码
```ssh
  git merge Yd/master
```

6. 最后把最新的代码推送到你的github上
```ssh
  git push origin master
```
