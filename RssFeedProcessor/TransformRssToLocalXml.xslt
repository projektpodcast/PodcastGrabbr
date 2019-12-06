<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:atom="http://www.w3.org/2005/Atom"
    xmlns:itunes="http://www.itunes.com/dtds/podcast-1.0.dtd">
  <xsl:output method="xml" indent="yes"/>




  <xsl:template match="/rss">
    <xsl:element name="podcasts">
      <xsl:apply-templates select="channel"/>
    </xsl:element>
  </xsl:template>


  <xsl:template match="channel">
    <xsl:element name="show">

      <!--<xsl:variable name="showidcounter" select="count(following-sibling::channel)" />-->
      <xsl:attribute name="sid">
        <xsl:text>new</xsl:text>
        <!--<xsl:value-of select="mycv"/>-->
      </xsl:attribute>
      <xsl:element name="title">
        <xsl:value-of select="title"/>
      </xsl:element>

      <xsl:element name="link">
        <xsl:value-of select="atom:link/@href"/>
      </xsl:element>

      <xsl:element name="allepisodes">

        <xsl:apply-templates select="item"></xsl:apply-templates>
      </xsl:element>
    </xsl:element>



  </xsl:template>


  <xsl:template match="item">


    <xsl:variable name="episodeidcounter" select="count(following-sibling::item) + 1" />

    <!--<xsl:variable name="level" select="count(/rss/channel/*)"/>-->
    <xsl:element name="episode">
      <xsl:attribute name="eid">

        <xsl:value-of select="$episodeidcounter"/>
        
          <!--<xsl:value-of select="$level"/>-->
        <!--<xsl:valueof ></xsl:valueof>-->
        <!--<xsl:value-of select="1"></xsl:value-of>-->
      </xsl:attribute>

      <xsl:element name="title">
        <xsl:apply-templates select="title"/>
      </xsl:element>
      <xsl:element name="summary">
        <xsl:apply-templates select="description"/>
      </xsl:element>
      <xsl:element name="url">
        <xsl:apply-templates select="enclosure/@url"/>
      </xsl:element>
      <xsl:element name="path">
      </xsl:element>

    </xsl:element>
  </xsl:template>


</xsl:stylesheet>
