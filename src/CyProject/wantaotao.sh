#echo "��һ������fastq�ļ���$1"
#echo "�ڶ�������,ref.fa:$2"
#echo "����������,����0Ϊ����or1Ϊ˫:$3"
#echo "���ĸ�������Ϊid��ʾ �������λ��"
#��������:
#./test.sh /data/home/guest/wantt/wroot/script/53I2_S6_L001_R1_001.00.0_0.cor.fastq /data/home/guest/wantt/wroot/script/ref.fa 0 aaaaa
basePath='/data/home/guest/wantt/wroot/AppData/'  #��Ŀ��ַ
echo "����Ŀ¼$basePath"
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


