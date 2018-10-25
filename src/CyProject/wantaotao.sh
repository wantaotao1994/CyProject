#echo "第一个参数fastq文件：$1"
#echo "第二个参数,ref.fa:$2"
#echo "第三个参数,类型0为单端or1为双:$3"
#echo "第四个参数，为id表示 结果拷贝位置"
#测试命令:
#./test.sh /data/home/guest/wantt/wroot/script/53I2_S6_L001_R1_001.00.0_0.cor.fastq /data/home/guest/wantt/wroot/script/ref.fa 0 aaaaa
basePath='/data/home/guest/wantt/wroot/AppData/'  #项目地址
echo "进入目录$basePath"
cd $basePath
ls
isnvPath="" #
isnvPath="illumina-isnv-ds"
refFaPath=$basePath/$isnvPath/sample_ref/ref.fa
echo "--------------------------------------------------------"
echo "path:$refFaPath"
fastQPath=$basePath/$isnvPath/clean_fastq
echo "--------------------------------------------------------"
echo "path:$fastQPath"
rm -f $fastQPath/*.fastq
cp $1 $fastQPath/
rm -f $refFaPath 
cp $2 $refFaPath
cd $isnvPath/sample_ref
bowtie2-build ref.fa ref
cd ..
echo  "----------------------"
ls
echo  "----------------------"
./iSNV_calling.sh
path=$4


